// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Azure.Messaging.ServiceBus;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Infrastructure;

public sealed class MessageFilterTests
{
    [Theory]
    [InlineData("sus.remos.establishment.create", true)]
    [InlineData("wrong label", false)]
    public void IsEstablishmentCreateMessage_ReturnsExpected(string label, bool expected)
    {
        // arrange
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(messageId: Guid.NewGuid().ToString(), subject: label);

        // act
        var result = MessageFilter.IsEstablishmentCreateMessage(message);

        // assert
        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData("sus.remos.establishment.update", true)]
    [InlineData("wrong label", false)]
    public void IsEstablishmentUpdateMessage_ReturnsExpected(string label, bool expected)
    {
        // arrange
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(messageId: Guid.NewGuid().ToString(), subject: label);

        // act
        var result = MessageFilter.IsEstablishmentUpdateMessage(message);

        // assert
        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData("sus.remos.signup", true)]
    [InlineData("wrong label", false)]
    public void IsSignUpCreateMessage_ReturnsExpected(string label, bool expected)
    {
        // arrange
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(messageId: Guid.NewGuid().ToString(), subject: label);

        // act
        var result = MessageFilter.IsSignUpCreateMessage(message);

        // assert
        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData("sus.remos.update", true)]
    [InlineData("wrong label", false)]
    public void IsSignUpUpdateMessage_ReturnsExpected(string label, bool expected)
    {
        // arrange
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(messageId: Guid.NewGuid().ToString(), subject: label);

        // act
        var result = MessageFilter.IsSignUpUpdateMessage(message);

        // assert
        result.ShouldBe(expected);
    }
}