// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public static class RemosValidationHelpers
{
    // Only validate it is a guid if it is not null. Null checks are handled by .NotNull()
    public static bool BeAGuid(string? value) => value is null || Guid.TryParse(value, out _);
}