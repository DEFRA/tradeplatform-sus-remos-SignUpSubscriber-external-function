// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.Common.AppConfig;
using Defra.Trade.Common.Logging.Extensions;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber;

public sealed class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.GetContext().Configuration;

        builder.Services
            .AddTradeAppConfiguration(configuration)
            .AddServiceRegistrations(configuration)
            .AddApplication()
            .AddFunctionLogging("RemosSignUpSubscriber");

        builder.ConfigureMapper();
    }

    [ExcludeFromCodeCoverage(Justification = "Little value in testing")]
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        builder.ConfigurationBuilder
            .ConfigureTradeAppConfiguration(config =>
            {
                config.UseKeyVaultSecrets = true;
                config.RefreshKeys.Add($"{RemosSignUpSubscriberSettings.RemosSignUpSubscriberSettingsName}:{RemosSignUpSubscriberSettings.AppConfigSentinelName}");
            });
    }
}
