// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Update;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class EstablishmentUpdateRequestValidator : AbstractValidator<Request>
{
    public EstablishmentUpdateRequestValidator(
        IValidator<TradeParty?> partyValidator)
    {
        RuleFor(m => m.TradeParty)
            .SetValidator(partyValidator);
    }
}