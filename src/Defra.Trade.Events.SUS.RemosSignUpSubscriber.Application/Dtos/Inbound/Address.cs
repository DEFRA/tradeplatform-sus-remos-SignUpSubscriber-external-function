// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;

public sealed class Address
{
    public string? CityName { get; set; }
    public string? County { get; set; }
    public Guid? Id { get; set; }
    public string? LineFive { get; set; }
    public string? LineFour { get; set; }
    public string? LineOne { get; set; }
    public string? LineThree { get; set; }
    public string? LineTwo { get; set; }
    public string? PostCode { get; set; }
    public string? TradeCountry { get; set; }
}