// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class AuthorisedSignatoryValidator : AbstractValidator<AuthorisedSignatory>
{
    public AuthorisedSignatoryValidator()
    {
        RuleFor(m => m.Name)
            .MaximumLength(100);

        RuleFor(m => m.Position)
            .MaximumLength(100);

        RuleFor(m => m.EmailAddress)
            .MaximumLength(100);
    }
}