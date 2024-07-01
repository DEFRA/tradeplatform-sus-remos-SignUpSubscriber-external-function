// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Common.Config;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Infrastructure;

public sealed class ServiceBusQueuesSettings : ServiceBusSettings
{
    public string? QueueNameEhcoRemosEnrichment { get; set; }
    public string? QueueNameEhcoRemosCreate { get; set; }
}
