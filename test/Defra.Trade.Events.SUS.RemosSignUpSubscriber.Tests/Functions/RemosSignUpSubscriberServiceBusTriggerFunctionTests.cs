// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Azure.Messaging.ServiceBus;
using Defra.Trade.Common.Functions.Interfaces;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;
using FakeItEasy;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Xunit;
using ExecutionContext = Microsoft.Azure.WebJobs.ExecutionContext;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Functions;

public sealed class RemosSignUpSubscriberServiceBusTriggerFunctionTests
{
    private readonly IMessageExecutorFactory _messageExecutorFactory;
    private readonly RemosSignUpSubscriberServiceBusTriggerFunction _sut;

    public RemosSignUpSubscriberServiceBusTriggerFunctionTests()
    {
        _messageExecutorFactory = A.Fake<IMessageExecutorFactory>(opt => opt.Strict());
        _sut = new RemosSignUpSubscriberServiceBusTriggerFunction(_messageExecutorFactory);
    }

    [Theory]
    [InlineData("sus.remos.signup")]
    [InlineData("sus.remos.update")]
    [InlineData("sus.remos.establishment.create")]
    [InlineData("sus.remos.establishment.update")]
    public async Task RunAsync_WithKnownLabel_ProcessesMessage(string label)
    {
        // arrange
        string messageId = Guid.NewGuid().ToString();
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(messageId: messageId, subject: label);
        var invocationId = Guid.NewGuid();
        string functionName = Guid.NewGuid().ToString();
        var actions = A.Fake<ServiceBusMessageActions>(opt => opt.Strict());
        var context = new ExecutionContext { InvocationId = invocationId, FunctionName = functionName };
        var eventStore = A.Fake<IAsyncCollector<ServiceBusMessage>>(opt => opt.Strict());

        var logger = A.Fake<ILogger>();
        var executor = A.Fake<IMessageExecutor>();

        var executeCall = A.CallTo(() => executor.ExecuteAsync(message, actions, context, eventStore, null!, null!, RemosSignUpSubscriberSettings.PublisherId, RemosSignUpSubscriberSettings.DefaultQueueName));
        var createMessageExecutorCall = A.CallTo(() => _messageExecutorFactory.CreateMessageExecutor(message));

        executeCall.Returns(Task.CompletedTask);
        createMessageExecutorCall.Returns(executor);

        // act
        await _sut.RunAsync(message, actions, context, eventStore, logger);

        // assert
        createMessageExecutorCall.MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task RunAsync_WithUnknownLabel_Throws()
    {
        // arrange
        string messageId = Guid.NewGuid().ToString();
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(messageId: messageId, subject: "abcxyz");
        var invocationId = Guid.NewGuid();
        string functionName = Guid.NewGuid().ToString();
        var actions = A.Fake<ServiceBusMessageActions>(opt => opt.Strict());
        var context = new ExecutionContext { InvocationId = invocationId, FunctionName = functionName };
        var eventStore = A.Fake<IAsyncCollector<ServiceBusMessage>>(opt => opt.Strict());

        var logger = A.Fake<ILogger>();
        var executor = A.Fake<IMessageExecutor>();

        var executeCall = A.CallTo(() => executor.ExecuteAsync(message, actions, context, eventStore, null!, null!, RemosSignUpSubscriberSettings.PublisherId, RemosSignUpSubscriberSettings.DefaultQueueName));
        var createMessageExecutorCall = A.CallTo(() => _messageExecutorFactory.CreateMessageExecutor(message));

        executeCall.Returns(Task.CompletedTask);
        createMessageExecutorCall.Returns(executor);

        // act
        await _sut.RunAsync(message, actions, context, eventStore, logger);

        // assert
        createMessageExecutorCall.MustHaveHappenedOnceExactly();
    }
}
