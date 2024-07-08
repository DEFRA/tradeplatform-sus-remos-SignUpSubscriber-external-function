// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class LogisticsLocationValidator : AbstractValidator<LogisticsLocation>
{
    public LogisticsLocationValidator(
        IValidator<Address?> addressValidator)
    {
        RuleFor(m => m.RemosEstablishmentSchemeNumber)
            .MaximumLength(25);

        RuleFor(m => m.Name)
            .MaximumLength(100);

        RuleFor(m => m.Address)
            .SetValidator(addressValidator);
    }
}
