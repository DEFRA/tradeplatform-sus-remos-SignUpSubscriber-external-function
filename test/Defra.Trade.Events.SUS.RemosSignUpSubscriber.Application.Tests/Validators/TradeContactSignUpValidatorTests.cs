// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public class TradeContactTests : ValidatorTestBase<TradeContactSignUp>
{
    private readonly TradeContactSignUpValidator _sut;

    public TradeContactTests()
    {
        _sut = new TradeContactSignUpValidator();
    }

    public static TheoryData<ModelSetter[], ErrorDetails[]> GetTestData()
    {
        var noErrors = Array.Empty<ErrorDetails>();
        return new()
        {
            { Array.Empty<ModelSetter>(), noErrors },
            { new[]{ Set(x => x.PersonName, null) }, noErrors },
            { new[]{ Set(x => x.PersonName, "abc") }, noErrors },
            { new[]{ Set(x => x.PersonName, new string('a', 50)) }, noErrors },
            { new[]{ Set(x => x.PersonName, new string('a', 51)) }, new ErrorDetails[]{ new("PersonName", "The length of 'Person Name' must be 50 characters or fewer. You entered 51 characters.") } },
            { new[]{ Set(x => x.Position, null) }, noErrors },
            { new[]{ Set(x => x.Position, "abc") }, noErrors },
            { new[]{ Set(x => x.Position, new string('a', 50)) }, noErrors },
            { new[]{ Set(x => x.Position, new string('a', 51)) }, new ErrorDetails[]{ new("Position", "The length of 'Position' must be 50 characters or fewer. You entered 51 characters.") } },
            { new[]{ Set(x => x.Email, null) }, noErrors },
            { new[]{ Set(x => x.Email, "abc") }, noErrors },
            { new[]{ Set(x => x.Email, new string('a', 100)) }, noErrors },
            { new[]{ Set(x => x.Email, new string('a', 101)) }, new ErrorDetails[]{ new("Email", "The length of 'Email' must be 100 characters or fewer. You entered 101 characters.") } },
            { new[]{ Set(x => x.TelephoneNumber, null) }, noErrors },
            { new[]{ Set(x => x.TelephoneNumber, "abc") }, noErrors },
            { new[]{ Set(x => x.TelephoneNumber, new string('a', 20)) }, noErrors },
            { new[]{ Set(x => x.TelephoneNumber, new string('a', 21)) }, new ErrorDetails[]{ new("TelephoneNumber", "The length of 'Telephone Number' must be 20 characters or fewer. You entered 21 characters.") } },
        };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ReturnsTheExpectedErrors(ModelSetter[] setters, ErrorDetails[] expected)
    {
        // arrange
        var contact = new TradeContactSignUp()
        {
            PersonName = Guid.NewGuid().ToString(),
            Position = Guid.NewGuid().ToString(),
            Email = Guid.NewGuid().ToString(),
            TelephoneNumber = Guid.NewGuid().ToString()[..10]
        };
        foreach (var setter in setters)
        {
            setter.ApplyTo(contact);
        }

        // act
        var result = _sut.Validate(contact);

        // assert
        string.Join("\n", result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
            .ShouldBe(string.Join("\n", expected.Select(e => $"{e.Property}: {e.Error}")));
    }
}
