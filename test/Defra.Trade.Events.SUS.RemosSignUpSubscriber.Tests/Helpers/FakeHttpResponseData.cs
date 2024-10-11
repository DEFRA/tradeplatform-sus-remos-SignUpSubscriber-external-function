// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.UnitTests.Helpers;

public class FakeHttpResponseData(FunctionContext functionContext) : HttpResponseData(functionContext)
{
    public override HttpStatusCode StatusCode { get; set; }

    public override HttpHeadersCollection Headers { get; set; } = [];

    public override Stream Body { get; set; } = new MemoryStream();

    public override HttpCookies Cookies { get; } = null!;
}
