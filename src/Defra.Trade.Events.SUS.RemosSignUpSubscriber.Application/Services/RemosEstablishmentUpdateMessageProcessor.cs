// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoMapper;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Crm;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Update;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Services;

public sealed class RemosEstablishmentUpdateMessageProcessor : BaseMessageProcessor<Request, MessageHeader, RemosEstablishmentUpdateMessageProcessor>
{
    private static readonly Action<ILogger, Exception?> _logMappingDone = LoggerMessage.Define(LogLevel.Information, default, "Mapping inspection location inbound messages to dynamics data structures succeeded");
    private static readonly Action<ILogger, Exception> _logMappingError = LoggerMessage.Define(LogLevel.Information, default, "Mapping inspection location inbound messages to dynamics data structures failed");
    private static readonly Action<ILogger, Exception?> _logMappingStart = LoggerMessage.Define(LogLevel.Information, default, "Mapping inspection location inbound messages to dynamics data structures");
    private static readonly Action<ILogger, Exception?> _logSendToDynamicsDone = LoggerMessage.Define(LogLevel.Information, default, "Sending inspection location to dynamics succeeded");
    private static readonly Action<ILogger, Exception> _logSendToDynamicsError = LoggerMessage.Define(LogLevel.Information, default, "Sending inspection location to dynamics failed");
    private static readonly Action<ILogger, Exception?> _logSendToDynamicsStart = LoggerMessage.Define(LogLevel.Information, default, "Sending inspection location to dynamics");
    private readonly ICrmClient _client;
    private readonly ILogger<RemosEstablishmentUpdateMessageProcessor> _logger;
    private readonly IMapper _mapper;

    public RemosEstablishmentUpdateMessageProcessor(
        ICrmClient client,
        IMapper mapper,
        ILogger<RemosEstablishmentUpdateMessageProcessor> logger)
        : base(client, mapper, logger)
    {
        _client = client;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<StatusResponse<Request>> ProcessAsync(Request messageRequest, MessageHeader messageHeader)
    {
        _logMappingStart(_logger, null);
        var inspectionLocation = MapToDynamicsModels(messageRequest);

        inspectionLocation.StatusCode = (int?)StatusCode.Inactive;
        inspectionLocation.StateCode = (int?)StateCode.Inactive;

        _logMappingDone(_logger, null);
        _logSendToDynamicsStart(_logger, null);

        try
        {
            await _client.UpdateAsync(inspectionLocation);
        }
        catch (Exception ex)
        {
            _logSendToDynamicsError(_logger, ex);
            throw;
        }

        _logSendToDynamicsDone(_logger, null);

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
            _logMappingError(_logger, ex);
            throw;
        }
    }
}