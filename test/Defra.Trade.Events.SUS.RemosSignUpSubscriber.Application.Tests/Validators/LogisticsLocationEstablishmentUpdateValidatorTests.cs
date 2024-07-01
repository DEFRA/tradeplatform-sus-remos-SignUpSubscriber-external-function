// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class LogisticsLocationEstablishmentUpdateValidatorTests : ValidatorTestBase<LogisticsLocationEstablishmentUpdate>
{
    private readonly LogisticsLocationEstablishmentUpdateValidator _sut;

    public LogisticsLocationEstablishmentUpdateValidatorTests()
    {
        _sut = new LogisticsLocationEstablishmentUpdateValidator();
    }

    public static TheoryData<ModelSetter[], ErrorDetails[]> GetTestData()
    {
        var noErrors = Array.Empty<ErrorDetails>();
        return new()
        {
            { Array.Empty<ModelSetter>(), noErrors },
            { new[] { Set(x => x.Id, Guid.NewGuid()) }, noErrors },
            { new[] { Set(x => x.InspectionLocationId, Guid.NewGuid().ToString()) }, noErrors },
            { new[] { Set(x => x.Status, "Removed") }, noErrors },
            { new[] { Set(x => x.TradePartyId, Guid.NewGuid().ToString()) }, noErrors },

            { new[] { Set(x => x.InspectionLocationId, null) }, new ErrorDetails[] { new("InspectionLocationId", "Inspection Location Id cannot be null") } },
            { new[] { Set(x => x.Status, null) }, new ErrorDetails[] { new("Status", "Status cannot be null\nStatus: Status must be Removed") } },
            { new[] { Set(x => x.TradePartyId, null) }, new ErrorDetails[] { new("TradePartyId", "Trade Party Id cannot be null") } },

            { new[] { Set(x => x.InspectionLocationId, "notAGuid") }, new ErrorDetails[] { new("InspectionLocationId", "Inspection Location Id is not a valid guid") } },
            { new[] { Set(x => x.Status, "notRemoved") }, new ErrorDetails[] { new("Status", "Status must be Removed") } },
            { new[] { Set(x => x.TradePartyId, "notAGuid") }, new ErrorDetails[] { new("TradePartyId", "Trade Party Id is not a valid guid") } },
        };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ReturnsTheExpectedErrors(ModelSetter[] setters, ErrorDetails[] expected)
    {
        // arrange
        var id = Guid.NewGuid();
        var inspectionLocationId = Guid.NewGuid().ToString();
        var status = "Removed";
        var tradePartyId = Guid.NewGuid().ToString();

        var location = new LogisticsLocationEstablishmentUpdate()
        {
            Id = id,
            InspectionLocationId = inspectionLocationId,
            Status = status,
            TradePartyId = tradePartyId
        };

        foreach (var setter in setters)
        {
            setter.ApplyTo(location);
        }

        // act
        var result = _sut.Validate(location);

        // assert
        string.Join("\n", result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
            .ShouldBe(string.Join("\n", expected.Select(e => $"{e.Property}: {e.Error}")));
    }
}