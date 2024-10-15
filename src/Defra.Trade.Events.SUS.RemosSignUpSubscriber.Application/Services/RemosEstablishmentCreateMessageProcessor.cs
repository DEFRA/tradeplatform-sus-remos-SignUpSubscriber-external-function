// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Crm;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Create;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Extensions;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Services;

public sealed class RemosEstablishmentCreateMessageProcessor(
    ICrmClient client,
    IMapper mapper,
    ILogger<RemosEstablishmentCreateMessageProcessor> logger) : BaseMessageProcessor<Request, MessageHeader, RemosEstablishmentCreateMessageProcessor>(client, mapper, logger)
{
    private readonly ICrmClient _client = client;
    private readonly ILogger<RemosEstablishmentCreateMessageProcessor> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public override async Task<StatusResponse<Request>> ProcessAsync(Request messageRequest, MessageHeader messageHeader)
    {
        _logger.EstablishmentCreateProcessorMappingStart(messageHeader.OrganisationId);
        var inspectionLocation = MapToDynamicsModels(messageRequest);
        _logger.EstablishmentCreateProcessorMappingSuccess(messageHeader.OrganisationId);

        _logger.EstablishmentCreateProcessorSendToDynamicsStart(messageHeader.OrganisationId);

        try
        {
            await _client.CreateAsync(inspectionLocation);
        }
        catch (Exception ex)
        {
            _logger.EstablishmentCreateProcessorSendToDynamicsFailure(ex, messageHeader.OrganisationId);
            throw;
        }

        _logger.EstablishmentCreateProcessorSendToDynamicsSuccess(messageHeader.OrganisationId);

        return new()
        {
            ForwardMessage = false,
            Response = messageRequest
        };
    }

    private InspectionLocation MapToDynamicsModels(Request messageRequest)
    {
        try
        {
            return _mapper.Map<InspectionLocation>(messageRequest);
        }
        catch (Exception ex)
        {
            _logger.EstablishmentCreateProcessorMappingFailure(ex);
            throw;
        }
    }
}
