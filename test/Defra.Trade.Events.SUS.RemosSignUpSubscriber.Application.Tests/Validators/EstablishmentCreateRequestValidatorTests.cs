// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Create;
using FakeItEasy;
using FluentValidation;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class EstablishmentCreateRequestValidatorTests : ValidatorTestBase<Request>
{
    private readonly IValidator<TradeParty> _partyValidator;
    private readonly EstablishmentCreateRequestValidator _sut;

    public EstablishmentCreateRequestValidatorTests()
    {
        _partyValidator = A.Fake<IValidator<TradeParty?>>(opt => opt.Strict());
        _sut = new EstablishmentCreateRequestValidator(_partyValidator!);
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