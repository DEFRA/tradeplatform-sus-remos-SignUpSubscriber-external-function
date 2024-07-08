// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Update;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Extensions;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class SignUpUpdateMessageHeaderValidator : AbstractValidator<MessageHeader>
{
    public SignUpUpdateMessageHeaderValidator()
    {
        this.AddCommonMessageHeaderValidation();

        RuleFor(m => m.SchemaVersion)
            .Equal(RemosSignUpServiceHeaderConstants.SignUp.Update.SchemaVersion).WithMessage(RemosValidationMessages.EqualField);

        RuleFor(m => m.Status)
            .Equal(RemosSignUpServiceHeaderConstants.SignUp.Update.Status).WithMessage(RemosValidationMessages.EqualField);

        RuleFor(m => m.Label)
            .Equal(RemosSignUpServiceHeaderConstants.SignUp.Update.Label).WithMessage(RemosValidationMessages.EqualField);
    }
}