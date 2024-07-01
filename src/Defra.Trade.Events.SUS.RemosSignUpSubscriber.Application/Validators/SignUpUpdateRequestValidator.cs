// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Update;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class SignUpUpdateRequestValidator : AbstractValidator<Request>
{
    public SignUpUpdateRequestValidator(
        IValidator<TradeParty?> tradePartyUpdateValidator)
    {
        RuleFor(m => m.TradeParty)
            .SetValidator(tradePartyUpdateValidator);
    }
}