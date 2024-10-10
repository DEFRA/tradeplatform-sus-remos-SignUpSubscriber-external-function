﻿// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Defra.Trade.Common.Function.Health.Extensions;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Functions;

/// <summary>
/// A http function that checks the health status of the function app.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="HealthCheckFunction"/> class.
/// </remarks>
/// <param name="healthCheckService">Health check service instance.</param>
public class HealthCheckFunction(HealthCheckService healthCheckService)
{
    private readonly HealthCheckService _healthCheckService = healthCheckService;

    /// <summary>
    /// Runs the http health check function.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [FunctionName(nameof(HealthCheckFunction))]
    public async Task<IActionResult> RunAsync(
#pragma warning disable IDE0060 // Remove unused parameter
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest request)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        var healthReport = await _healthCheckService.CheckHealthAsync();

        if (healthReport.Status == HealthStatus.Healthy)
        {
            return new JsonResult("Healthy");
        }

        var healthCheckResponse = healthReport.ToResponse();

        return new JsonResult(healthCheckResponse);
    }
}
