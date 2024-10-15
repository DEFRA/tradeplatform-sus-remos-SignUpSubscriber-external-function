// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Crm;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Update;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Extensions;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Services
{
    public sealed class RemosUpdateMessageProcessor(ICrmClient client, IMapper mapper, ILogger<RemosUpdateMessageProcessor> logger) : BaseMessageProcessor<Request, MessageHeader, RemosUpdateMessageProcessor>(client, mapper, logger)
    {
        private readonly ICrmClient _client = client;
        private readonly ILogger<RemosUpdateMessageProcessor> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public override async Task<StatusResponse<Request>> ProcessAsync(Request messageRequest, MessageHeader messageHeader)
        {
            _logger.UpdateMessageProcessorMappingStart(messageHeader.OrganisationId);
            var organisation = MapToDynamicsModels(messageRequest);
            _logger.UpdateMessageProcessorMappingSuccess(messageHeader.OrganisationId);

            _logger.UpdateMessageProcessorSendToDynamicsStart(messageHeader.OrganisationId);

            try
            {
                await _client.UpdateAsync(organisation);
            }
            catch (Exception ex)
            {
                _logger.UpdateMessageProcessorSendToDynamicsFailure(ex, messageHeader.OrganisationId);
                throw;
            }

            _logger.UpdateMessageProcessorSendToDynamicsSuccess(messageHeader.OrganisationId);

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
                return _mapper.Map<OrganisationUpdate>(messageRequest);
            }
            catch (Exception ex)
            {
                _logger.UpdateMessageProcessorMappingFailure(ex);
                throw;
            }
        }
    }
}
