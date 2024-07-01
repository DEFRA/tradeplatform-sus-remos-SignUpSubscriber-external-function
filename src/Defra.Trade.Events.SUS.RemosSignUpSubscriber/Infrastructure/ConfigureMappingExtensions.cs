// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Linq;
using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Infrastructure;

public static class ConfigureMappingExtensions
{
    public static void ConfigureMapper(this IFunctionsHostBuilder hostBuilder)
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName is string fullName && fullName.Contains("Defra"))
            .OrderBy(a => a.FullName)
            .ToList();
        hostBuilder.Services.AddAutoMapper(assembly);
    }
}