// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Immutable;
using AutoMapper;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Mapping;

public sealed class DynamicsCountryProfile : Profile
{
    public static readonly IReadOnlyDictionary<string, Country> Mapping = new Dictionary<string, Country>
    {
        ["England"] = Country.England,
        ["Scotland"] = Country.Scotland,
        ["Wales"] = Country.Wales,
        ["NorthernIreland"] = Country.NorthernIreland,
        ["Northern Ireland"] = Country.NorthernIreland,
        ["NI"] = Country.NorthernIreland
    }.ToImmutableDictionary(StringComparer.OrdinalIgnoreCase);

    public DynamicsCountryProfile()
    {
        CreateMap<string?, Country?>()
            .ConvertUsing(value => value == null ? null : Mapping[value]);
    }
}