// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoMapper;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Crm;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Update;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Services
{
    public sealed class RemosUpdateMessageProcessor : BaseMessageProcessor<Request, MessageHeader, RemosUpdateMessageProcessor>
    {
        private static readonly Action<ILogger, Exception?> _logMappingDone = LoggerMessage.Define(LogLevel.Information, default, "Mapping update inbound messages to dynamics data structures succeeded");
        private static readonly Action<ILogger, Exception> _logMappingError = LoggerMessage.Define(LogLevel.Information, default, "Mapping update inbound messages to dynamics data structures failed");
        private static readonly Action<ILogger, Exception?> _logMappingStart = LoggerMessage.Define(LogLevel.Information, default, "Mapping update inbound messages to dynamics data structures");
        private static readonly Action<ILogger, Exception?> _logSendToDynamicsDone = LoggerMessage.Define(LogLevel.Information, default, "Sending update organisation location to dynamics succeeded");
        private static readonly Action<ILogger, Exception> _logSendToDynamicsError = LoggerMessage.Define(LogLevel.Information, default, "Sending update organisation location to dynamics failed");
        private static readonly Action<ILogger, Exception?> _logSendToDynamicsStart = LoggerMessage.Define(LogLevel.Information, default, "Sending update organisation location to dynamics");
        private readonly ICrmClient _client;
        private readonly ILogger<RemosUpdateMessageProcessor> _logger;
        private readonly IMapper _mapper;

        public RemosUpdateMessageProcessor(ICrmClient client, IMapper mapper, ILogger<RemosUpdateMessageProcessor> logger)
            : base(client, mapper, logger)
        {
            _client = client;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<StatusResponse<Request>> ProcessAsync(Request messageRequest, MessageHeader messageHeader)
        {
            _logMappingStart(_logger, null);
            var organisation = MapToDynamicsModels(messageRequest);
            _logMappingDone(_logger, null);

            _logSendToDynamicsStart(_logger, null);

            try
            {
                await _client.UpdateAsync(organisation);
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

        private OrganisationUpdate MapToDynamicsModels(Request messageRequest)
        {
            try
            {
                return (
                    _mapper.Map<OrganisationUpdate>(messageRequest)
                );
            }
            catch (Exception ex)
            {
                _logMappingError(_logger, ex);
                throw;
            }
        }
    }
}