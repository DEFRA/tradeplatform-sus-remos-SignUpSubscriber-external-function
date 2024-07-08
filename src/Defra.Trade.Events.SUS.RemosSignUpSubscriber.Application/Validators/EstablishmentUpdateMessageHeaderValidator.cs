// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Update;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Extensions;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class EstablishmentUpdateMessageHeaderValidator : AbstractValidator<MessageHeader>
{
    public EstablishmentUpdateMessageHeaderValidator()
    {
        this.AddCommonMessageHeaderValidation();

        RuleFor(m => m.SchemaVersion)
            .Equal(RemosSignUpServiceHeaderConstants.Establishment.Update.SchemaVersion).WithMessage(RemosValidationMessages.EqualField);

        RuleFor(m => m.Status)
            .Equal(RemosSignUpServiceHeaderConstants.Establishment.Update.Status).WithMessage(RemosValidationMessages.EqualField);

        RuleFor(m => m.Label)
            .Equal(RemosSignUpServiceHeaderConstants.Establishment.Update.Label).WithMessage(RemosValidationMessages.EqualField);
    }
}