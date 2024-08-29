// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Create;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Mapping;

public sealed class RemosSignUpRequestProfile : Profile
{
    private const int DynamicsMinYear = 1753;

    public RemosSignUpRequestProfile()
    {
        CreateMap<Request, OrganisationSignup>()
            .ForMember(dest => dest.Id, m => m.MapFrom(src => src.TradeParty!.OrgId))
            .ForMember(dest => dest.LastSubmittedBy, m => m.MapFrom(src => src.TradeParty!.SignUpRequestSubmittedBy))
            .ForMember(dest => dest.BaseCountry, m => m.MapFrom(src => src.TradeParty!.CountryName))
            .ForMember(dest => dest.RmsBusinessSchemeNumber, m => m.MapFrom(src => src.TradeParty!.RemosBusinessSchemeNumber))
            .ForMember(dest => dest.FboNumber, m => m.MapFrom(src => src.TradeParty!.FboNumber))
            .ForMember(dest => dest.PhrNumber, m => m.MapFrom(src => src.TradeParty!.PhrNumber))
            .ForMember(dest => dest.RmsBusinessContactName, m => m.MapFrom(src => src.TradeParty!.TradeContact!.PersonName))
            .ForMember(dest => dest.RmsBusinessContactPosition, m => m.MapFrom(src => src.TradeParty!.TradeContact!.Position))
            .ForMember(dest => dest.RmsBusinessContactEmail, m => m.MapFrom(src => src.TradeParty!.TradeContact!.Email))
            .ForMember(dest => dest.RmsBusinessContactTelephone, m => m.MapFrom(src => src.TradeParty!.TradeContact!.TelephoneNumber))
            .ForMember(dest => dest.AuthorisedSignatoryName, m => m.MapFrom(src => src.TradeParty!.AuthorisedSignatory!.Name))
            .ForMember(dest => dest.AuthorisedSignatoryPosition, m => m.MapFrom(src => src.TradeParty!.AuthorisedSignatory!.Position))
            .ForMember(dest => dest.AuthorisedSignatoryEmail, m => m.MapFrom(src => src.TradeParty!.AuthorisedSignatory!.EmailAddress))
            .ForMember(dest => dest.RmsTAndCsAccepted, m => m.MapFrom(src => src.TradeParty!.TermsAndConditionsSignedDate > DateTimeOffset.MinValue))
            .ForMember(dest => dest.RmsSignUpRequestSubmittedOn, m => m.MapFrom(src => CutoffOldDates(src.TradeParty!.TermsAndConditionsSignedDate)))
            .ForMember(dest => dest.RmsApprovalStatus, m => m.MapFrom(src => null as RmsApproval?));

        CreateMap<Request, IEnumerable<InspectionLocation>>()
            .ConvertUsing((src, dest, ctx) => src.TradeParty!.LogisticsLocations!.Select(l => ctx.Mapper.Map<InspectionLocation>(new TradePartyLocation(src.TradeParty, l))));

        CreateMap<TradePartyLocation, InspectionLocation>()
            .ForMember(dest => dest.Id, m => m.Ignore())
            .ForMember(dest => dest.OrganisationId, m => m.MapFrom(src => src.TradeParty!.OrgId))
            .ForMember(dest => dest.LastSubmittedBy, m => m.MapFrom(src => src.TradeParty!.SignUpRequestSubmittedBy))
            .ForMember(dest => dest.RmsEstablishmentNumber, m => m.MapFrom(src => src.LogisticsLocation!.RemosEstablishmentSchemeNumber))
            .ForMember(dest => dest.LocationName, m => m.MapFrom(src => src.LogisticsLocation!.Name))
            .ForMember(dest => dest.AddressLine1, m => m.MapFrom(src => src.LogisticsLocation!.Address!.LineOne))
            .ForMember(dest => dest.AddressLine2, m => m.MapFrom(src => src.LogisticsLocation!.Address!.LineTwo))
            .ForMember(dest => dest.City, m => m.MapFrom(src => src.LogisticsLocation!.Address!.CityName))
            .ForMember(dest => dest.Country, m => m.MapFrom(src => src.LogisticsLocation!.Address!.County))
            .ForMember(dest => dest.Postcode, m => m.MapFrom(src => src.LogisticsLocation!.Address!.PostCode))
            .ForMember(dest => dest.ContactEmailAddress, m => m.MapFrom(src => src.TradeParty!.TradeContact!.Email))
            .ForMember(dest => dest.LocationType, m => m.MapFrom(src => LocationType.Establishment))
            .ForMember(dest => dest.StateCode, m => m.Ignore())
            .ForMember(dest => dest.StatusCode, m => m.Ignore());
    }

    private static DateTimeOffset? CutoffOldDates(DateTimeOffset? value)
    {
        return value switch
        {
            null or { Year: < DynamicsMinYear } => null,
            _ => value
        };
    }

    private record struct TradePartyLocation(TradeParty TradeParty, LogisticsLocation LogisticsLocation);
}