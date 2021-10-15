// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using bVirtualization.Models.BVirutalizationComponents;
using bVirtualization.Views.Components;
using FluentAssertions;
using Xunit;

namespace bVirtualization.Tests.Unit.Views.Components.BVirtualizations
{
    public partial class BVirtualizationComponentTests
    {
        [Fact]
        public void ShouldInitializeComponent()
        {
            // given
            BVirutalizationComponentState expectedState =
                BVirutalizationComponentState.Loading;

            // when
            var initialBVirtualizationComponent =
                new BVirtualizationComponent<object>();

            // then
            initialBVirtualizationComponent.State.Should().Be(expectedState);
            initialBVirtualizationComponent.VirtualizeService.Should().BeNull();
            initialBVirtualizationComponent.ChildContent.Should().BeNull();
            initialBVirtualizationComponent.Label.Should().BeNull();
            initialBVirtualizationComponent.ErrorMessage.Should().BeNull();
        }
    }
}
