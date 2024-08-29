// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class TradeContactSignUpValidator : AbstractValidator<TradeContactSignUp>
{
    public TradeContactSignUpValidator()
    {
        RuleFor(m => m.PersonName)
            .MaximumLength(50);

        RuleFor(m => m.Position)
            .MaximumLength(50);

        RuleFor(m => m.Email)
            .MaximumLength(100);

        RuleFor(m => m.TelephoneNumber)
            .MaximumLength(20);
    }
}