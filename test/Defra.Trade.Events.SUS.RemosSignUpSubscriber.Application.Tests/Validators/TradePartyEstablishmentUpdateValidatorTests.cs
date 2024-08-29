// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.Establishment.Update;
using FakeItEasy;
using FluentValidation;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public sealed class TradePartyEstablishmentUpdateValidatorTests : ValidatorTestBase<TradeParty>
{
    private readonly IValidator<LogisticsLocationEstablishmentUpdate?> _logisticsLocationValidator;
    private readonly TradePartyEstablishmentUpdateValidator _sut;

    public TradePartyEstablishmentUpdateValidatorTests()
    {
        _logisticsLocationValidator = A.Fake<IValidator<LogisticsLocationEstablishmentUpdate?>>(opt => opt.Strict());
        _sut = new TradePartyEstablishmentUpdateValidator(_logisticsLocationValidator);
    }

    public static TheoryData<ModelSetter[], ErrorDetails[]> GetTestData()
    {
        var noErrors = Array.Empty<ErrorDetails>();
        return new()
        {
             { Array.Empty<ModelSetter>(), noErrors },
             { new[] { Set(x => x.OrgId, null) }, new ErrorDetails[] { new("Org Id", "cannot be null") } },
             { new[] { Set(x => x.OrgId, Guid.NewGuid().ToString()) }, noErrors },
             { new[] { Set(x => x.Id, Guid.NewGuid().ToString()) }, noErrors },
             { new[] { Set(x => x.Id, null) }, new ErrorDetails[] { new("Id", "cannot be null") } },
         };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ReturnsExpectedErrors(ModelSetter[] setters, ErrorDetails[] expected)
    {
        // Arrange
        var logisticsLocation = new LogisticsLocationEstablishmentUpdate();
        string id = Guid.NewGuid().ToString();
        string orgId = Guid.NewGuid().ToString();

        var establishmentCreate = new TradeParty()
        {
            Id = id,
            OrgId = orgId,
            LogisticsLocation = logisticsLocation
        };

        var call = A.CallTo(() => _logisticsLocationValidator.Validate(A<ValidationContext<LogisticsLocationEstablishmentUpdate>>.That.Matches(ctx => ctx.InstanceToValidate == logisticsLocation)));
        call.Returns(new());

        foreach (var setter in setters)
        {
            setter.ApplyTo(establishmentCreate);
        }

        // Act
        var result = _sut.Validate(establishmentCreate);

        // Assert
        string.Join("\n", result.Errors.Select(e => $"{e.ErrorMessage}"))
            .ShouldBe(string.Join("\n", expected.Select(e => $"{e.Property} {e.Error}")));

        call.MustHaveHappenedOnceExactly();
    }
}
