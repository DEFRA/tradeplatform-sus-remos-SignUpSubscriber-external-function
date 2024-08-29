// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Create;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Mapping;
using FluentValidation;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class TradePartySignUpCreateValidator : AbstractValidator<TradeParty>
{
    public TradePartySignUpCreateValidator(
        IValidator<TradeContactSignUp?> contactValidator,
        IValidator<AuthorisedSignatory?> authorisedSignatoryValidator,
        IValidator<LogisticsLocation?> logisticsLocationValidator)
    {
        RuleFor(m => m.CountryName)
            .Must(BeAMappableCountryName).WithMessage($"{{PropertyName}} must be one of {string.Join(", ", DynamicsCountryProfile.Mapping.Keys.OrderBy(n => n))}");

        RuleFor(m => m.RemosBusinessSchemeNumber)
            .MaximumLength(25);

        RuleFor(m => m.FboNumber)
            .MaximumLength(25);

        RuleFor(m => m.PhrNumber)
            .MaximumLength(25);

        RuleFor(m => m.TradeContact)
            .SetValidator(contactValidator);

        RuleFor(m => m.AuthorisedSignatory)
            .SetValidator(authorisedSignatoryValidator);

        RuleFor(m => m.LogisticsLocations)
            .ForEach(m => m.SetValidator(logisticsLocationValidator));
    }

    private static bool BeAMappableCountryName(string? value) => value is null || DynamicsCountryProfile.Mapping.ContainsKey(value);
}