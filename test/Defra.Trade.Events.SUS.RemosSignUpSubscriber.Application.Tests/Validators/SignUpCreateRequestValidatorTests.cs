// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Create;
using FakeItEasy;
using FluentValidation;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class SignUpCreateRequestValidatorTests
{
    private readonly IValidator<TradeParty?> _partyValidator;
    private readonly SignUpCreateRequestValidator _sut;

    public SignUpCreateRequestValidatorTests()
    {
        _partyValidator = A.Fake<IValidator<TradeParty?>>(opt => opt.Strict());
        _sut = new SignUpCreateRequestValidator(_partyValidator);
    }

    [Fact]
    public void Validate_DefersToThePartyValidator()
    {
        // arrange
        var party = new TradeParty();
        var model = new Request { TradeParty = party };
        var partyValidate = A.CallTo(() => _partyValidator.Validate(A<ValidationContext<TradeParty>>.That.Matches(ctx => ctx.InstanceToValidate == party)));
        partyValidate.Returns(new());

        // act
        var result = _sut.Validate(model);

        // assert
        result.IsValid.ShouldBe(true);
        partyValidate.MustHaveHappenedOnceExactly();
    }
}