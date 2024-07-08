// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public class AddressValidatorTests : ValidatorTestBase<Address>
{
    private readonly AddressValidator _sut;

    public AddressValidatorTests()
    {
        _sut = new AddressValidator();
    }

    public static TheoryData<ModelSetter[], ErrorDetails[]> GetTestData()
    {
        var noErrors = Array.Empty<ErrorDetails>();
        return new()
        {
            { Array.Empty<ModelSetter>(), noErrors },
            { new[]{ Set(x => x.LineOne, null) }, noErrors },
            { new[]{ Set(x => x.LineOne, "abc") }, noErrors },
            { new[]{ Set(x => x.LineOne, new string('a', 250)) }, noErrors },
            { new[]{ Set(x => x.LineOne, new string('a', 251)) }, new ErrorDetails[]{ new("LineOne", "The length of 'Line One' must be 250 characters or fewer. You entered 251 characters.") } },
            { new[]{ Set(x => x.LineTwo, null) }, noErrors },
            { new[]{ Set(x => x.LineTwo, "abc") }, noErrors },
            { new[]{ Set(x => x.LineTwo, new string('a', 250)) }, noErrors },
            { new[]{ Set(x => x.LineTwo, new string('a', 251)) }, new ErrorDetails[]{ new("LineTwo", "The length of 'Line Two' must be 250 characters or fewer. You entered 251 characters.") } },
            { new[]{ Set(x => x.LineThree, null) }, noErrors },
            { new[]{ Set(x => x.LineThree, "abc") }, noErrors },
            { new[]{ Set(x => x.LineThree, new string('a', 250)) }, noErrors },
            { new[]{ Set(x => x.LineThree, new string('a', 251)) }, new ErrorDetails[]{ new("LineThree", "The length of 'Line Three' must be 250 characters or fewer. You entered 251 characters.") } },
            { new[]{ Set(x => x.LineFour, null) }, noErrors },
            { new[]{ Set(x => x.LineFour, "abc") }, noErrors },
            { new[]{ Set(x => x.LineFour, new string('a', 250)) }, noErrors },
            { new[]{ Set(x => x.LineFour, new string('a', 251)) }, new ErrorDetails[]{ new("LineFour", "The length of 'Line Four' must be 250 characters or fewer. You entered 251 characters.") } },
            { new[]{ Set(x => x.LineFive, null) }, noErrors },
            { new[]{ Set(x => x.LineFive, "abc") }, noErrors },
            { new[]{ Set(x => x.LineFive, new string('a', 250)) }, noErrors },
            { new[]{ Set(x => x.LineFive, new string('a', 251)) }, new ErrorDetails[]{ new("LineFive", "The length of 'Line Five' must be 250 characters or fewer. You entered 251 characters.") } },
            { new[]{ Set(x => x.CityName, null) }, noErrors },
            { new[]{ Set(x => x.CityName, "abc") }, noErrors },
            { new[]{ Set(x => x.CityName, new string('a', 250)) }, noErrors },
            { new[]{ Set(x => x.CityName, new string('a', 251)) }, new ErrorDetails[]{ new("CityName", "The length of 'City Name' must be 250 characters or fewer. You entered 251 characters.") } },
            { new[]{ Set(x => x.County, null) }, noErrors },
            { new[]{ Set(x => x.County, "abc") }, noErrors },
            { new[]{ Set(x => x.County, new string('a', 100)) }, noErrors },
            { new[]{ Set(x => x.County, new string('a', 101)) }, new ErrorDetails[]{ new("County", "The length of 'County' must be 100 characters or fewer. You entered 101 characters.") } },
            { new[]{ Set(x => x.PostCode, null) }, noErrors },
            { new[]{ Set(x => x.PostCode, "abc") }, noErrors },
            { new[]{ Set(x => x.PostCode, new string('a', 20)) }, noErrors },
            { new[]{ Set(x => x.PostCode, new string('a', 21)) }, new ErrorDetails[]{ new("PostCode", "The length of 'Post Code' must be 20 characters or fewer. You entered 21 characters.") } },
        };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ReturnsTheExpectedErrors(ModelSetter[] setters, ErrorDetails[] expected)
    {
        // arrange
        var address = new Address()
        {
            LineOne = Guid.NewGuid().ToString(),
            LineTwo = Guid.NewGuid().ToString(),
            LineThree = Guid.NewGuid().ToString(),
            LineFour = Guid.NewGuid().ToString(),
            LineFive = Guid.NewGuid().ToString(),
            CityName = Guid.NewGuid().ToString(),
            County = Guid.NewGuid().ToString(),
            PostCode = Guid.NewGuid().ToString()[..10]
        };
        foreach (var setter in setters)
        {
            setter.ApplyTo(address);
        }

        // act
        var result = _sut.Validate(address);

        // assert
        string.Join("\n", result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
            .ShouldBe(string.Join("\n", expected.Select(e => $"{e.Property}: {e.Error}")));
    }
}