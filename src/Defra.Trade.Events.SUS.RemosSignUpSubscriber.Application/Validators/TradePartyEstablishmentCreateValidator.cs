﻿// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Functions.Validation;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Create;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class TradePartyEstablishmentCreateValidator : AbstractValidator<TradeParty>
{
    public TradePartyEstablishmentCreateValidator(
        IValidator<LogisticsLocation?> logisticsLocationValidator)
    {
        RuleFor(m => m.OrgId)
            .NotNull().WithMessage(ValidationMessages.NullField)
            .Must(RemosValidationHelpers.BeAGuid).WithMessage(RemosValidationMessages.GuidField);

        RuleFor(m => m.LogisticsLocation)
            .SetValidator(logisticsLocationValidator);
    }
}