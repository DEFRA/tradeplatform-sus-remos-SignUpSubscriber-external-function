// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Common.Functions.Extensions;
using Defra.Trade.Common.Functions.Interfaces;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Extensions;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Functions;

public sealed class RemosSignUpSubscriberServiceBusTriggerFunction
{
    private readonly IMessageExecutorFactory _messageExecutorFactory;

    public RemosSignUpSubscriberServiceBusTriggerFunction(IMessageExecutorFactory messageExecutorFactory)
    {
        _messageExecutorFactory = messageExecutorFactory;
    }

    [ServiceBusAccount(RemosSignUpSubscriberSettings.ConnectionStringConfigurationKey)]
    [FunctionName(nameof(RemosSignUpSubscriberServiceBusTriggerFunction))]
    public async Task RunAsync(
        [ServiceBusTrigger(queueName: RemosSignUpSubscriberSettings.DefaultQueueName, IsSessionsEnabled = false)] ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions,
        ExecutionContext executionContext,
        [ServiceBus(RemosSignUpSubscriberSettings.TradeEventInfo)] IAsyncCollector<ServiceBusMessage> eventStoreCollector,
        ILogger logger)
    {
        await RunInternalAsync(message, messageActions, eventStoreCollector, executionContext, logger);
    }

    private static string? GetCrmRequestType(ServiceBusReceivedMessage message) => message.Label() switch
    {
        RemosSignUpServiceHeaderConstants.SignUp.Create.Label => CrmRequestConstants.Create,
        RemosSignUpServiceHeaderConstants.SignUp.Update.Label => CrmRequestConstants.Update,
        RemosSignUpServiceHeaderConstants.Establishment.Create.Label => CrmRequestConstants.Create,
        RemosSignUpServiceHeaderConstants.Establishment.Update.Label => CrmRequestConstants.Update,
        _ => throw new ArgumentOutOfRangeException(nameof(message))
    };

    private async Task RunInternalAsync(
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageReceiver,
        IAsyncCollector<ServiceBusMessage> eventStoreCollector,
        ExecutionContext executionContext,
        ILogger logger)
    {
        try
        {
            logger.MessageReceived(message.MessageId, executionContext.FunctionName);

            await _messageExecutorFactory
                .CreateMessageExecutor(message)
                .ExecuteAsync(
                    message,
                    messageReceiver,
                    executionContext,
                    eventStoreCollector,
                    RemosSignUpSubscriberSettings.DefaultQueueName,
                    RemosSignUpSubscriberSettings.PublisherId,
                    RemosSignUpSubscriberSettings.PublisherId,
                    GetCrmRequestType(message));

            logger.LogInformation("Finished processing Messages Id : {MessageId} received on {FunctionName}", message.MessageId, executionContext.FunctionName);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, ex.Message);
        }
    }
}
