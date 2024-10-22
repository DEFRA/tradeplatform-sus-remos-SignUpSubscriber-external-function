// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Mapping;

public class DynamicsCountryProfileTests
{
    private readonly IMapper _sut;

    public DynamicsCountryProfileTests()
    {
        _sut = new MapperConfiguration(ctx =>
        {
            ctx.AddProfile<DynamicsCountryProfile>();
        }).CreateMapper();
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("England", Country.England)]
    [InlineData("england", Country.England)]
    [InlineData("Scotland", Country.Scotland)]
    [InlineData("scotland", Country.Scotland)]
    [InlineData("Wales", Country.Wales)]
    [InlineData("wales", Country.Wales)]
    [InlineData("Northern Ireland", Country.NorthernIreland)]
    [InlineData("northern ireland", Country.NorthernIreland)]
    [InlineData("NorthernIreland", Country.NorthernIreland)]
    [InlineData("northernireland", Country.NorthernIreland)]
    [InlineData("NI", Country.NorthernIreland)]
    [InlineData("ni", Country.NorthernIreland)]
    public void Map_Maps(string? from, Country? to)
    {
        // arrange

        // act
        var actual = _sut.Map<Country?>(from);

        // assert
        actual.ShouldBe(to);
    }
}