// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;

public sealed class AuthorisedSignatory
{
    public string? EmailAddress { get; set; }
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Position { get; set; }
    public Guid? TradePartyId { get; set; }
}