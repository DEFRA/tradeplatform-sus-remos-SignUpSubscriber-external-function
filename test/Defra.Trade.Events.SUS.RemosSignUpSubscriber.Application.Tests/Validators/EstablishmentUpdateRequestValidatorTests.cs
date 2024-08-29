// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Update;
using FakeItEasy;
using FluentValidation;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class EstablishmentUpdateRequestValidatorTests : ValidatorTestBase<Request>
{
    private readonly IValidator<TradeParty> _partyValidator;
    private readonly EstablishmentUpdateRequestValidator _sut;

    public EstablishmentUpdateRequestValidatorTests()
    {
        _partyValidator = A.Fake<IValidator<TradeParty?>>(opt => opt.Strict());
        _sut = new EstablishmentUpdateRequestValidator(_partyValidator!);
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