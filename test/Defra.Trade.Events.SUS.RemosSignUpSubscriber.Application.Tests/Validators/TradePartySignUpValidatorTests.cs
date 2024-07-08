// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Create;
using FakeItEasy;
using FluentValidation;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public class TradePartyTests : ValidatorTestBase<TradeParty>
{
    private readonly IValidator<AuthorisedSignatory?> _authorisedSignatoryValidator;
    private readonly IValidator<TradeContactSignUp?> _contactValidator;
    private readonly IValidator<LogisticsLocation?> _logisticsLocationValidator;
    private readonly TradePartySignUpCreateValidator _sut;

    public TradePartyTests()
    {
        _contactValidator = A.Fake<IValidator<TradeContactSignUp?>>(opt => opt.Strict());
        _authorisedSignatoryValidator = A.Fake<IValidator<AuthorisedSignatory?>>(opt => opt.Strict());
        _logisticsLocationValidator = A.Fake<IValidator<LogisticsLocation?>>(opt => opt.Strict());
        _sut = new TradePartySignUpCreateValidator(_contactValidator, _authorisedSignatoryValidator, _logisticsLocationValidator);
    }

    public static TheoryData<ModelSetter[], ErrorDetails[]> GetTestData()
    {
        var noErrors = Array.Empty<ErrorDetails>();
        return new()
        {
            { Array.Empty<ModelSetter>(), noErrors },
            { new[]{ Set(x => x.RemosBusinessSchemeNumber, null) }, noErrors },
            { new[]{ Set(x => x.RemosBusinessSchemeNumber, "abc") }, noErrors },
            { new[]{ Set(x => x.RemosBusinessSchemeNumber, new string('a', 25)) }, noErrors },
            { new[]{ Set(x => x.RemosBusinessSchemeNumber, new string('a', 26)) }, new ErrorDetails[]{ new("RemosBusinessSchemeNumber", "The length of 'Remos Business Scheme Number' must be 25 characters or fewer. You entered 26 characters.") } },
            { new[]{ Set(x => x.FboNumber, null) }, noErrors },
            { new[]{ Set(x => x.FboNumber, "abc") }, noErrors },
            { new[]{ Set(x => x.FboNumber, new string('a', 25)) }, noErrors },
            { new[]{ Set(x => x.FboNumber, new string('a', 26)) }, new ErrorDetails[]{ new("FboNumber", "The length of 'Fbo Number' must be 25 characters or fewer. You entered 26 characters.") } },
            { new[]{ Set(x => x.PhrNumber, null) }, noErrors },
            { new[]{ Set(x => x.PhrNumber, "abc") }, noErrors },
            { new[]{ Set(x => x.PhrNumber, new string('a', 25)) }, noErrors },
            { new[]{ Set(x => x.PhrNumber, new string('a', 26)) }, new ErrorDetails[]{ new("PhrNumber", "The length of 'Phr Number' must be 25 characters or fewer. You entered 26 characters.") } },
            { new[]{ Set(x => x.CountryName, null) }, noErrors },
            { new[]{ Set(x => x.CountryName, "England") }, noErrors },
            { new[]{ Set(x => x.CountryName, "england") }, noErrors },
            { new[]{ Set(x => x.CountryName, "Scotland") }, noErrors },
            { new[]{ Set(x => x.CountryName, "scotland") }, noErrors },
            { new[]{ Set(x => x.CountryName, "Wales") }, noErrors },
            { new[]{ Set(x => x.CountryName, "wales") }, noErrors },
            { new[]{ Set(x => x.CountryName, "NI") }, noErrors },
            { new[]{ Set(x => x.CountryName, "ni") }, noErrors },
            { new[]{ Set(x => x.CountryName, "Northern Ireland") }, noErrors },
            { new[]{ Set(x => x.CountryName, "northern ireland") }, noErrors },
            { new[]{ Set(x => x.CountryName, "northernireland") }, noErrors },
            { new[]{ Set(x => x.CountryName, "NorthernIreland") }, noErrors },
            { new[]{ Set(x => x.CountryName, "abc") }, new ErrorDetails[]{ new("CountryName", "Country Name must be one of England, NI, Northern Ireland, NorthernIreland, Scotland, Wales") } },
        };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ReturnsTheExpectedErrors(ModelSetter[] setters, ErrorDetails[] expected)
    {
        // arrange
        var contact = new TradeContactSignUp();
        var authorisedSignatory = new AuthorisedSignatory();
        var logisticsLocations = new LogisticsLocation[] { new(), new(), new() };

        var party = new TradeParty()
        {
            CountryName = "England",
            RemosBusinessSchemeNumber = Guid.NewGuid().ToString()[..10],
            FboNumber = Guid.NewGuid().ToString()[..10],
            PhrNumber = Guid.NewGuid().ToString()[..10],
            TradeContact = contact,
            AuthorisedSignatory = authorisedSignatory,
            LogisticsLocations = logisticsLocations.ToArray()
        };

        var calls = new[]
        {
            A.CallTo(() => _contactValidator.Validate(A<ValidationContext<TradeContactSignUp>>.That.Matches(ctx => ctx.InstanceToValidate == contact))),
            A.CallTo(() => _authorisedSignatoryValidator.Validate(A<ValidationContext<AuthorisedSignatory>>.That.Matches(ctx => ctx.InstanceToValidate == authorisedSignatory))),
            A.CallTo(() => _logisticsLocationValidator.Validate(A<ValidationContext<LogisticsLocation>>.That.Matches(ctx => ctx.InstanceToValidate == logisticsLocations[0]))),
            A.CallTo(() => _logisticsLocationValidator.Validate(A<ValidationContext<LogisticsLocation>>.That.Matches(ctx => ctx.InstanceToValidate == logisticsLocations[1]))),
            A.CallTo(() => _logisticsLocationValidator.Validate(A<ValidationContext<LogisticsLocation>>.That.Matches(ctx => ctx.InstanceToValidate == logisticsLocations[2]))),
        };

        foreach (var call in calls)
        {
            call.Returns(new());
        }

        foreach (var setter in setters)
        {
            setter.ApplyTo(party);
        }

        // act
        var result = _sut.Validate(party);

        // assert
        string.Join("\n", result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
            .ShouldBe(string.Join("\n", expected.Select(e => $"{e.Property}: {e.Error}")));
        foreach (var call in calls)
        {
            call.MustHaveHappenedOnceExactly();
        }
    }
}