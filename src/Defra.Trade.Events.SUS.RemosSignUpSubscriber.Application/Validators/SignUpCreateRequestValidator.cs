// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Create;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class SignUpCreateRequestValidator : AbstractValidator<Request>
{
    public SignUpCreateRequestValidator(
        IValidator<TradeParty?> partyValidator)
    {
        RuleFor(m => m.TradeParty)
            .SetValidator(partyValidator);
    }
}