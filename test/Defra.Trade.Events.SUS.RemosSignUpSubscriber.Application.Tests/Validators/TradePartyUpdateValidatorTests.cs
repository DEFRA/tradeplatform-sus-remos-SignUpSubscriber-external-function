// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Update;
using FakeItEasy;
using FluentValidation;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class TradePartyUpdateValidatorTests : ValidatorTestBase<TradeParty>
{
    private readonly IValidator<AuthorisedSignatory?> _authorisedSignatoryValidator;
    private readonly IValidator<TradeContactUpdate?> _contactValidator;
    private readonly TradePartySignUpValidator _sut;

    public TradePartyUpdateValidatorTests()
    {
        _contactValidator = A.Fake<IValidator<TradeContactUpdate?>>(opt => opt.Strict());
        _authorisedSignatoryValidator = A.Fake<IValidator<AuthorisedSignatory?>>(opt => opt.Strict());
        _sut = new TradePartySignUpValidator(_contactValidator, _authorisedSignatoryValidator);
    }

    public static TheoryData<ModelSetter[], ErrorDetails[]> GetTestData()
    {
        var noErrors = Array.Empty<ErrorDetails>();

        return new()
        {
            { Array.Empty<ModelSetter>(), noErrors },
            { new[] { Set(x => x.OrgId, null) }, new ErrorDetails[] { new("Org Id", "cannot be null") } },
            { new[] { Set(x => x.OrgId, Guid.NewGuid().ToString()) }, noErrors },
            { new[] { Set(x => x.SignUpRequestSubmittedBy, Guid.NewGuid().ToString()) }, noErrors },
            { new[] { Set(x => x.SignUpRequestSubmittedBy, null) }, new ErrorDetails[] { new("Sign Up Request Submitted By", "cannot be null") } },
        };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ReturnsExpectedErrors(ModelSetter[] setters, ErrorDetails[] expected)
    {
        // Arrange
        var contact = new TradeContactUpdate();
        var authorisedSignatory = new AuthorisedSignatory();

        var partyUpdate = new TradeParty()
        {
            AuthorisedSignatory = authorisedSignatory,
            OrgId = Guid.NewGuid().ToString(),
            SignUpRequestSubmittedBy = Guid.NewGuid().ToString(),
            TradeContact = contact,
        };

        var calls = new[]
        {
            A.CallTo(() => _contactValidator.Validate(A<ValidationContext<TradeContactUpdate>>.That.Matches(ctx => ctx.InstanceToValidate == contact))),
            A.CallTo(() => _authorisedSignatoryValidator.Validate(A<ValidationContext<AuthorisedSignatory>>.That.Matches(ctx => ctx.InstanceToValidate == authorisedSignatory)))
        };

        foreach (var call in calls)
        {
            call.Returns(new());
        }

        foreach (var setter in setters)
        {
            setter.ApplyTo(partyUpdate);
        }

        // Act
        var result = _sut.Validate(partyUpdate);

        // Assert
        string.Join("\n", result.Errors.Select(e => $"{e.ErrorMessage}"))
            .ShouldBe(string.Join("\n", expected.Select(e => $"{e.Property} {e.Error}")));

        foreach (var call in calls)
        {
            call.MustHaveHappenedOnceExactly();
        }
    }
}