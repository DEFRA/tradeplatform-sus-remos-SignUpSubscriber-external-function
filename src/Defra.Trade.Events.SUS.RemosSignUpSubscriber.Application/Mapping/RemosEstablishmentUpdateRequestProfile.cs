// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoMapper;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Update;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Mapping;

public sealed class RemosEstablishmentUpdateRequestProfile : Profile
{
    public RemosEstablishmentUpdateRequestProfile()
    {
        CreateMap<Request, InspectionLocation>()
            .ForMember(dest => dest.AddressLine1, m => m.Ignore())
            .ForMember(dest => dest.AddressLine2, m => m.Ignore())
            .ForMember(dest => dest.City, m => m.Ignore())
            .ForMember(dest => dest.ContactEmailAddress, m => m.Ignore())
            .ForMember(dest => dest.Country, m => m.Ignore())
            .ForMember(dest => dest.Id, m => m.MapFrom(src => src.TradeParty!.LogisticsLocation!.InspectionLocationId))
            .ForMember(dest => dest.LastSubmittedBy, m => m.Ignore())
            .ForMember(dest => dest.LocationName, m => m.Ignore())
            .ForMember(dest => dest.LocationType, m => m.Ignore())
            .ForMember(dest => dest.OrganisationId, m => m.MapFrom(src => src.TradeParty!.OrgId))
            .ForMember(dest => dest.Postcode, m => m.Ignore())
            .ForMember(dest => dest.RmsEstablishmentNumber, m => m.Ignore())
            .ForMember(dest => dest.StateCode, m => m.Ignore())
            .ForMember(dest => dest.StatusCode, m => m.Ignore());
    }
}