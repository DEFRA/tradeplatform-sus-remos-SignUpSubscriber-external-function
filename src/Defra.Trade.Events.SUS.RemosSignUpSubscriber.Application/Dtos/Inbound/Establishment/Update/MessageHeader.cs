﻿// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Interfaces;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Update;

public sealed class MessageHeader : TradeEventMessageHeader, ICommonMessageHeader
{
}