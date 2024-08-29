// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(m => m.LineOne)
            .MaximumLength(250);

        RuleFor(m => m.LineTwo)
            .MaximumLength(250);

        RuleFor(m => m.LineThree)
            .MaximumLength(250);

        RuleFor(m => m.LineFour)
            .MaximumLength(250);

        RuleFor(m => m.LineFive)
            .MaximumLength(250);

        RuleFor(m => m.CityName)
            .MaximumLength(250);

        RuleFor(m => m.County)
            .MaximumLength(100);

        RuleFor(m => m.PostCode)
            .MaximumLength(20);
    }
}