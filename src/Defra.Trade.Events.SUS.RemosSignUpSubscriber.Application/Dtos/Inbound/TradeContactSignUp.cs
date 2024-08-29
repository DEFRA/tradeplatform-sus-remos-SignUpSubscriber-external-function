// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;

public sealed class TradeContactSignUp
{
    public string? Email { get; set; }
    public Guid? Id { get; set; }
    public bool? IsAuthorisedSignatory { get; set; }
    public string? PersonName { get; set; }
    public string? Position { get; set; }
    public string? TelephoneNumber { get; set; }
    public Guid? TradePartyId { get; set; }
}