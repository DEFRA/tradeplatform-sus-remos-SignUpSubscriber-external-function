// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;

public sealed class LogisticsLocationEstablishmentCreate
{
    public Address? Address { get; set; }
    public string? Email { get; set; }
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? RemosEstablishmentSchemeNumber { get; set; }
    public Guid? TradePartyId { get; set; }
}