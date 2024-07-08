// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Functions;
using FakeItEasy;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber;

public sealed class StartupTests
{
    private readonly Startup _sut;

    public StartupTests()
    {
        _sut = new Startup();
    }

    [Fact]
    public void Configure_ResutsInAValidConfiguration()
    {
        // arrange
        var context = new WebJobsBuilderContext();
        var webJobs = A.Fake<IWebJobsBuilder>(opt => opt.Strict());
        var config = new ConfigurationBuilder()
            .Build();
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddScoped<RemosSignUpSubscriberServiceBusTriggerFunction>();

        context.Configuration = config;
        A.CallTo(() => webJobs.Services).Returns(services);

        var builder = CreateHostBuilder(context, webJobs);

        // act
        _sut.Configure(builder);

        // assert
        services.ShouldNotBeEmpty();
        services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true,
            ValidateScopes = true
        });
    }

    private static IFunctionsHostBuilder CreateHostBuilder(WebJobsBuilderContext context, IWebJobsBuilder webJobs)
    {
        var startup = A.Fake<FunctionsStartup>(opt => opt.Strict());
        IFunctionsHostBuilder? functionsBuilder = null;
        var captureFunctionsHostBuilder = (IFunctionsHostBuilder b) =>
        {
            functionsBuilder = b;
            return true;
        };
        A.CallTo(() => startup.Configure(A<IFunctionsHostBuilder>.That.Matches(captureFunctionsHostBuilder, "NO-OP"))).DoesNothing();
        startup.Configure(context, webJobs);
        return functionsBuilder!;
    }
}