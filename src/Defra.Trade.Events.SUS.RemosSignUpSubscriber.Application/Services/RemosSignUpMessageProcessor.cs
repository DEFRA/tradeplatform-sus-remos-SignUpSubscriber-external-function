// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Runtime.ExceptionServices;
using AutoMapper;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Crm;
using Defra.Trade.Crm.Clients;
using Defra.Trade.Crm.Exceptions;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Create;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Services;

public sealed class RemosSignUpMessageProcessor : BaseMessageProcessor<Request, MessageHeader, RemosSignUpMessageProcessor>
{
    private static readonly Action<ILogger, Exception?> _logMappingDone = LoggerMessage.Define(LogLevel.Information, default, "Mapping signup inbound messages to dynamics data structures succeeded");
    private static readonly Action<ILogger, Exception> _logMappingError = LoggerMessage.Define(LogLevel.Information, default, "Mapping signup inbound messages to dynamics data structures failed");
    private static readonly Action<ILogger, Exception?> _logMappingStart = LoggerMessage.Define(LogLevel.Information, default, "Mapping signup inbound messages to dynamics data structures");
    private static readonly Action<ILogger, Exception?> _logSendToDynamicsDone = LoggerMessage.Define(LogLevel.Information, default, "Sending signup organisation and inspection location to dynamics succeeded");
    private static readonly Action<ILogger, Exception> _logSendToDynamicsError = LoggerMessage.Define(LogLevel.Information, default, "Sending signup organisation and inspection location to dynamics failed");
    private static readonly Action<ILogger, Exception?> _logSendToDynamicsStart = LoggerMessage.Define(LogLevel.Information, default, "Sending signup organisation and inspection location to dynamics");
    private readonly ICrmClient _client;
    private readonly ILogger<RemosSignUpMessageProcessor> _logger;
    private readonly IMapper _mapper;

    public RemosSignUpMessageProcessor(
        ICrmClient client,
        IMapper mapper,
        ILogger<RemosSignUpMessageProcessor> logger)
        : base(client, mapper, logger)
    {
        _client = client;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<StatusResponse<Request>> ProcessAsync(Request messageRequest, MessageHeader messageHeader)
    {
        _logMappingStart(_logger, null);
        var (organisation, inspectionLocations) = MapToDynamicsModels(messageRequest);
        _logMappingDone(_logger, null);

        _logSendToDynamicsStart(_logger, null);
        await SendToDynamics(organisation, inspectionLocations);
        _logSendToDynamicsDone(_logger, null);

        return new()
        {
            ForwardMessage = false,
            Response = messageRequest
        };
    }

    private static async Task<Exception?> SendToDynamics(IBoundCrmChangesetBuilder builder)
    {
        try
        {
            await builder.SendAsync();
            return null;
        }
        catch (CrmException ex)
        {
            return ex;
        }
    }

    private static async Task<Exception?> SendToDynamics(OrganisationSignup organisation, IBoundCrmChangesetBuilder builder)
    {
        try
        {
            builder.AddUpsert(organisation, out var task);
            await task;
            return null;
        }
        catch (CrmException ex)
        {
            return ExceptionDispatchInfo.SetCurrentStackTrace(
                new CrmException(ex.Code, ex.StatusCode ?? 0, $"Failed to create organisation {organisation.Id}", ex));
        }
    }

    private static async Task<Exception?> SendToDynamics(InspectionLocation location, IBoundCrmChangesetBuilder builder)
    {
        try
        {
            builder.AddCreate(location, out var task);
            await task;
            return null;
        }
        catch (CrmException ex)
        {
            return ExceptionDispatchInfo.SetCurrentStackTrace(
                new CrmException(ex.Code, ex.StatusCode ?? 0, $"Failed to create Inspection Location {location.RmsEstablishmentNumber}", ex));
        }
    }

    private (OrganisationSignup, IEnumerable<InspectionLocation>) MapToDynamicsModels(Request messageRequest)
    {
        try
        {
            return (
                _mapper.Map<OrganisationSignup>(messageRequest),
                _mapper.Map<IEnumerable<InspectionLocation>>(messageRequest)
            );
        }
        catch (Exception ex)
        {
            _logMappingError(_logger, ex);
            throw;
        }
    }

    private async Task SendToDynamics(OrganisationSignup organisation, IEnumerable<InspectionLocation> inspectionLocations)
    {
        try
        {
            var changeset = _client.Changeset();

            var tasks = inspectionLocations
                .Select(l => SendToDynamics(l, changeset))
                .Prepend(SendToDynamics(organisation, changeset))
                .ToList();

            tasks = tasks.Prepend(SendToDynamics(changeset))
                .ToList();

            var results = await Task.WhenAll(tasks);

            var errors = results.Where(e => e is not null)
                .ToList();

            if (errors.Count > 0)
                throw new AggregateException(errors!);
        }
        catch (Exception ex)
        {
            _logSendToDynamicsError(_logger, ex);
            throw;
        }
    }
}