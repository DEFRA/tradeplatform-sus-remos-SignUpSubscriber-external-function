// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Immutable;
using AutoMapper;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Mapping;

public sealed class DynamicsYesNoProfile : Profile
{
    public static readonly IReadOnlyDictionary<bool, YesNo> Mapping = new Dictionary<bool, YesNo>
    {
        [true] = YesNo.Yes,
        [false] = YesNo.No
    }.ToImmutableDictionary();

    public DynamicsYesNoProfile()
    {
        CreateMap<bool, YesNo>()
            .ConvertUsing(v => Mapping[v]);
    }
}