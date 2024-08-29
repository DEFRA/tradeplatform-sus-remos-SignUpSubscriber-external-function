// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;
using Shouldly;
using Xunit;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Mapping;

public class DynamicsYesNoProfileTests
{
    private readonly IMapper _sut;

    public DynamicsYesNoProfileTests()
    {
        _sut = new MapperConfiguration(ctx =>
        {
            ctx.AddProfile<DynamicsYesNoProfile>();
        }).CreateMapper();
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(true, YesNo.Yes)]
    [InlineData(false, YesNo.No)]
    public void Map_Maps(bool? from, YesNo? to)
    {
        // arrange

        // act
        var actual = _sut.Map<YesNo?>(from);

        // assert
        actual.ShouldBe(to);
    }
}