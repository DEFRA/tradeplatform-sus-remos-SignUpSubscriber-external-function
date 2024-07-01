// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Update;
using FakeItEasy;
using FluentValidation;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class SignUpUpdateRequestValidatorTests
{
    private readonly IValidator<TradeParty?> _partyUpdateValidator;
    private readonly SignUpUpdateRequestValidator _sut;

    public SignUpUpdateRequestValidatorTests()
    {
        _partyUpdateValidator = A.Fake<IValidator<TradeParty?>>(opt => opt.Strict());
        _sut = new SignUpUpdateRequestValidator(_partyUpdateValidator);
    }

    [Fact]
    public void Validate_DefersToPartyUpdateValidator()
    {
        // Arrange
        var party = new TradeParty();
        var model = new Request { TradeParty = party };
        var partyValidate = A.CallTo(() => _partyUpdateValidator.Validate(A<ValidationContext<TradeParty>>.That.Matches(ctx => ctx.InstanceToValidate == party)));
        partyValidate.Returns(new());

        // act
        var result = _sut.Validate(model);

        // assert
        result.IsValid.ShouldBe(true);
        partyValidate.MustHaveHappenedOnceExactly();
    }
}