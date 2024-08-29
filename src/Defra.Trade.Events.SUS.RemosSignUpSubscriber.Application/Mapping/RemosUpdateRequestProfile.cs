// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Update;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Mapping;

public sealed class RemosUpdateRequestProfile : Profile
{
    public RemosUpdateRequestProfile()
    {
        CreateMap<Request, OrganisationUpdate>()
            .ForMember(dest => dest.Id, m => m.MapFrom(src => src.TradeParty!.OrgId))
            .ForMember(dest => dest.LastSubmittedBy, m => m.MapFrom(src => src.TradeParty!.SignUpRequestSubmittedBy))
            .ForMember(dest => dest.RmsBusinessContactName, m => m.MapFrom(src => src.TradeParty!.TradeContact!.PersonName))
            .ForMember(dest => dest.RmsBusinessContactPosition, m => m.MapFrom(src => src.TradeParty!.TradeContact!.Position))
            .ForMember(dest => dest.RmsBusinessContactEmail, m => m.MapFrom(src => src.TradeParty!.TradeContact!.Email))
            .ForMember(dest => dest.RmsBusinessContactTelephone, m => m.MapFrom(src => src.TradeParty!.TradeContact!.TelephoneNumber))
            .ForMember(dest => dest.AuthorisedSignatoryName, m => m.MapFrom(src => src.TradeParty!.AuthorisedSignatory!.Name))
            .ForMember(dest => dest.AuthorisedSignatoryPosition, m => m.MapFrom(src => src.TradeParty!.AuthorisedSignatory!.Position))
            .ForMember(dest => dest.AuthorisedSignatoryEmail, m => m.MapFrom(src => src.TradeParty!.AuthorisedSignatory!.EmailAddress));
    }
}