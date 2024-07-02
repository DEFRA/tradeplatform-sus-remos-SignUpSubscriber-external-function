// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;

public sealed class RemosSignUpSubscriberSettings
{

    public const string RemosSignUpSubscriberSettingsName = "EhcoGcSubscriber";
#if DEBUG

    // In 'Debug' (locally) use connection string
    public const string ConnectionStringConfigurationKey = "Endpoint=https://devtrdinfac1001.azconfig.io;Id=sQZM-l8-s0:uPI644tgUHUr5tdP51Kp;Secret=oNBwRjAPyl5s8SfPFhusHTsFWXgS40BpOz+rTmCndJc=";

    public const string ConnectionStringConfigurationKeySecond = "Endpoint=https://devtrdinfac1001.azconfig.io;Id=sQZM-l8-s0:uPI644tgUHUr5tdP51Kp;Secret=oNBwRjAPyl5s8SfPFhusHTsFWXgS40BpOz+rTmCndJc=";

#else
    // Assumes that this is 'Release' and uses Managed Identity rather than connection string
    // ie it will actually bind to ServiceBus:FullyQualifiedNamespace !
    public const string ConnectionStringConfigurationKey = "ServiceBus";
#endif

    public const string DefaultQueueName = "defra.trade.sus.remos.signup";
    public const string PublisherId = "REMOS";
    public const string TradeEventInfo = Common.Functions.Constants.QueueName.DefaultEventsInfoQueueName;

    public const string AppConfigSentinelName = "Sentinel";
    public string RemosSignUpCreatedQueue { get; set; } = DefaultQueueName;
}
