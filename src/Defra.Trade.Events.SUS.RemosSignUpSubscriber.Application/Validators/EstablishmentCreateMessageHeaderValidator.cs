// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Create;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Extensions;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class EstablishmentCreateMessageHeaderValidator : AbstractValidator<MessageHeader>
{
    public EstablishmentCreateMessageHeaderValidator()
    {
        this.AddCommonMessageHeaderValidation();

        RuleFor(m => m.SchemaVersion)
            .Equal(RemosSignUpServiceHeaderConstants.Establishment.Create.SchemaVersion).WithMessage(RemosValidationMessages.EqualField);

        RuleFor(m => m.Status)
            .Equal(RemosSignUpServiceHeaderConstants.Establishment.Create.Status).WithMessage(RemosValidationMessages.EqualField);

        RuleFor(m => m.Label)
            .Equal(RemosSignUpServiceHeaderConstants.Establishment.Create.Label).WithMessage(RemosValidationMessages.EqualField);
    }
}