// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoMapper;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Create;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Mapping;

public sealed class RemosEstablishmentCreateRequestProfile : Profile
{
    public RemosEstablishmentCreateRequestProfile()
    {
        CreateMap<Request, InspectionLocation>()
            .ForMember(dest => dest.Id, m => m.Ignore())
            .ForMember(dest => dest.OrganisationId, m => m.MapFrom(src => src.TradeParty!.OrgId))
            .ForMember(dest => dest.RmsEstablishmentNumber, m => m.MapFrom(src => src.TradeParty!.LogisticsLocation!.RemosEstablishmentSchemeNumber))
            .ForMember(dest => dest.LocationName, m => m.MapFrom(src => src.TradeParty!.LogisticsLocation!.Name))
            .ForMember(dest => dest.AddressLine1, m => m.MapFrom(src => src.TradeParty!.LogisticsLocation!.Address!.LineOne))
            .ForMember(dest => dest.AddressLine2, m => m.MapFrom(src => src.TradeParty!.LogisticsLocation!.Address!.LineTwo))
            .ForMember(dest => dest.City, m => m.MapFrom(src => src.TradeParty!.LogisticsLocation!.Address!.CityName))
            .ForMember(dest => dest.Country, m => m.MapFrom(src => src.TradeParty!.LogisticsLocation!.Address!.County))
            .ForMember(dest => dest.Postcode, m => m.MapFrom(src => src.TradeParty!.LogisticsLocation!.Address!.PostCode))
            .ForMember(dest => dest.ContactEmailAddress, m => m.MapFrom(src => src.TradeParty!.LogisticsLocation!.Email))
            .ForMember(dest => dest.LocationType, m => m.MapFrom(src => LocationType.Establishment))
            .ForMember(dest => dest.LastSubmittedBy, m => m.Ignore())
            .ForMember(dest => dest.StateCode, m => m.Ignore())
            .ForMember(dest => dest.StatusCode, m => m.Ignore());
    }
}