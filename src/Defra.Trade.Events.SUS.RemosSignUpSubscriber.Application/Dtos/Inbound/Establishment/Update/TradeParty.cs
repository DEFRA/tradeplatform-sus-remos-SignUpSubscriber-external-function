// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Update;

public sealed class TradeParty
{
    public string? Id { get; set; }
    public LogisticsLocationEstablishmentUpdate? LogisticsLocation { get; set; }
    public string? OrgId { get; set; }
}