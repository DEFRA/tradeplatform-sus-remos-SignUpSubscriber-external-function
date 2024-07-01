// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Create;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class EstablishmentCreateRequestValidator : AbstractValidator<Request>
{
    public EstablishmentCreateRequestValidator(
        IValidator<TradeParty?> partyValidator)
    {
        RuleFor(m => m.TradeParty)
            .SetValidator(partyValidator);
    }
}