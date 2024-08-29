// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class AuthorisedSignatoryValidatorTests : ValidatorTestBase<AuthorisedSignatory>
{
    private readonly AuthorisedSignatoryValidator _sut;

    public AuthorisedSignatoryValidatorTests()
    {
        _sut = new AuthorisedSignatoryValidator();
    }

    public static TheoryData<ModelSetter[], ErrorDetails[]> GetTestData()
    {
        var noErrors = Array.Empty<ErrorDetails>();
        return new()
        {
            { Array.Empty<ModelSetter>(), noErrors },
            { new[]{ Set(x => x.EmailAddress, null) }, noErrors },
            { new[]{ Set(x => x.EmailAddress, "abc") }, noErrors },
            { new[]{ Set(x => x.EmailAddress, new string('a', 100)) }, noErrors },
            { new[]{ Set(x => x.EmailAddress, new string('a', 101)) }, new ErrorDetails[]{ new("EmailAddress", "The length of 'Email Address' must be 100 characters or fewer. You entered 101 characters.") } },
            { new[]{ Set(x => x.Name, null) }, noErrors },
            { new[]{ Set(x => x.Name, "abc") }, noErrors },
            { new[]{ Set(x => x.Name, new string('a', 100)) }, noErrors },
            { new[]{ Set(x => x.Name, new string('a', 101)) }, new ErrorDetails[]{ new("Name", "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.") } },
            { new[]{ Set(x => x.Position, null) }, noErrors },
            { new[]{ Set(x => x.Position, "abc") }, noErrors },
            { new[]{ Set(x => x.Position, new string('a', 100)) }, noErrors },
            { new[]{ Set(x => x.Position, new string('a', 101)) }, new ErrorDetails[]{ new("Position", "The length of 'Position' must be 100 characters or fewer. You entered 101 characters.") } },
        };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ReturnsTheExpectedErrors(ModelSetter[] setters, ErrorDetails[] expected)
    {
        // arrange
        var signatory = new AuthorisedSignatory()
        {
            EmailAddress = Guid.NewGuid().ToString(),
            Position = Guid.NewGuid().ToString(),
            Name = Guid.NewGuid().ToString()
        };
        foreach (var setter in setters)
        {
            setter.ApplyTo(signatory);
        }

        // act
        var result = _sut.Validate(signatory);

        // assert
        string.Join("\n", result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
            .ShouldBe(string.Join("\n", expected.Select(e => $"{e.Property}: {e.Error}")));
    }
}