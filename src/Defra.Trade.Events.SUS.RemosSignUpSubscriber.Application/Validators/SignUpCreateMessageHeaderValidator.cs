// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Create;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Extensions;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class SignUpCreateMessageHeaderValidator : AbstractValidator<MessageHeader>
{
    public SignUpCreateMessageHeaderValidator()
    {
        this.AddCommonMessageHeaderValidation();

        RuleFor(m => m.SchemaVersion)
            .Equal(RemosSignUpServiceHeaderConstants.SignUp.Create.SchemaVersion).WithMessage(RemosValidationMessages.EqualField);

        RuleFor(m => m.Status)
            .Equal(RemosSignUpServiceHeaderConstants.SignUp.Create.Status).WithMessage(RemosValidationMessages.EqualField);

        RuleFor(m => m.Label)
            .Equal(RemosSignUpServiceHeaderConstants.SignUp.Create.Label).WithMessage(RemosValidationMessages.EqualField);
    }
}