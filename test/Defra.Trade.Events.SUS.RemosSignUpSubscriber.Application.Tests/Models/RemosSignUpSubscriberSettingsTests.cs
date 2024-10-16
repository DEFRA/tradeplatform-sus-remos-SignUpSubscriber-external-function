// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;

public class RemosSignUpSubscriberSettingsTests
{
    [Fact]
    public void Settings_ShouldBe_AsExpected()
    {
        // Act
        var sut = new RemosSignUpSubscriberSettings();

        // Assert
        RemosSignUpSubscriberSettings.RemosSignUpSubscriberSettingsName.ShouldBe("EhcoGcSubscriber");

        RemosSignUpSubscriberSettings.ConnectionStringConfigurationKey.ShouldBe("ServiceBus:ConnectionString");
        RemosSignUpSubscriberSettings.DefaultQueueName.ShouldBe("defra.trade.sus.remos.signup");
        RemosSignUpSubscriberSettings.PublisherId.ShouldBe("REMOS");
        RemosSignUpSubscriberSettings.TradeEventInfo.ShouldBe("defra.trade.events.info");
        RemosSignUpSubscriberSettings.AppConfigSentinelName.ShouldBe("Sentinel");
        sut.RemosSignUpCreatedQueue.ShouldBe("defra.trade.sus.remos.signup");
    }

}
