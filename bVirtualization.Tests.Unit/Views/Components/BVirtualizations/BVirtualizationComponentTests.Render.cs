// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;
using bVirtualization.Models.BVirutalizationComponents;
using bVirtualization.Views.Components;
using FluentAssertions;
using Moq;
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

        [Fact]
        public void ShouldRenderContent()
        {
            // given
            BVirutalizationComponentState expectedState =
                BVirutalizationComponentState.Content;

            IQueryable<object> randomData =
                CreateRandomQueryable();

            IQueryable<object> retrievedData =
                randomData;

            IQueryable<object> expectedData =
                retrievedData;

            this.virtualizationServiceMock.Setup(service =>
                service.LoadFirstPage(It.IsAny<uint>(), It.IsAny<uint>()))
                    .Returns(retrievedData);

            // when
            this.renderedComponent = 
                RenderComponent<BVirtualizationComponent<object>>();

            // then
            this.renderedComponent.Instance.State.Should().Be(expectedState);

            this.virtualizationServiceMock.Verify(service =>
                service.LoadFirstPage(It.IsAny<uint>(), It.IsAny<uint>()),
                    Times.Once);

            this.renderedComponent.Instance.ErrorMessage.Should().BeNull();
            this.renderedComponent.Instance.Label.Should().BeNull();
            this.virtualizationServiceMock.VerifyNoOtherCalls();
        }
    }
}
