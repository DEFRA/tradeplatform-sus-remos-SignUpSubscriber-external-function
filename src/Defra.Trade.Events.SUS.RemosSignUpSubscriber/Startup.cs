// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.Common.AppConfig;
using Defra.Trade.Common.Function.Health.HealthChecks;
using Defra.Trade.Common.Logging.Extensions;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Infrastructure;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Infrastructure;
using FunctionHealthCheck;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

        var healthChecksBuilder = builder.Services.AddFunctionHealthChecks();
        RegisterHealthChecks(healthChecksBuilder, builder.Services, configuration);

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

    private static void RegisterHealthChecks(
      IHealthChecksBuilder builder,
      IServiceCollection services,
      IConfiguration configuration)
    {
        builder.AddCheck<AppSettingHealthCheck>("ServiceBus:ConnectionString")
            .AddCheck<AppSettingHealthCheck>("ServiceBus:QueueNameEhcoRemosEnrichment");

        var serviceBusQueuesSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ServiceBusQueuesSettings>>();
       

        builder.AddAzureServiceBusCheck(configuration, "ServiceBus:ConnectionString", serviceBusQueuesSettings.Value.QueueNameEhcoRemosEnrichment);
        builder.AddAzureServiceBusCheck(configuration, "ServiceBus:ConnectionString", serviceBusQueuesSettings.Value.QueueNameEhcoRemosCreate);
    }
}
