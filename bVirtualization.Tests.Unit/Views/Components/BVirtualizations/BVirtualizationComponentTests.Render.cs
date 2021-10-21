// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using Bunit;
using bVirtualization.Models.BVirutalizationComponents;
using bVirtualization.Views.Components;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
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
            initialBVirtualizationComponent.DataSource.Should().BeNull();
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

            IQueryable<object> inputDataSource =
                randomData;

            IQueryable<object> expectedDataSource =
                inputDataSource;

            RenderFragment<object> inputChildContent =
                CreateRenderFragment(typeof(SomeComponent<object>));

            RenderFragment<object> expectedChildContent =
                inputChildContent;

            var componentParameters = new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(
                    nameof(BVirtualizationComponent<object>.ChildContent),
                    inputChildContent),

                ComponentParameter.CreateParameter(
                    nameof(BVirtualizationComponent<object>.DataSource),
                    inputDataSource)
            };

            // when
            this.renderedComponent =
                RenderComponent<BVirtualizationComponent<object>>(componentParameters);

            // then
            this.renderedComponent.Instance.State
                .Should().Be(expectedState);

            this.renderedComponent.Instance.ChildContent
                .Should().BeEquivalentTo(expectedChildContent);

            this.renderedComponent.Instance.DataSource.Should()
                .BeEquivalentTo(expectedDataSource);

            this.renderedComponent.FindComponents<SomeComponent<object>>()
                .Count().Should().Be(expectedDataSource.Count());

            this.renderedComponent.Instance.ErrorMessage.Should().BeNull();
            this.renderedComponent.Instance.Label.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderErrorWhenExceptionOccurs()
        {
            // given
            BVirutalizationComponentState expectedState =
                BVirutalizationComponentState.Error;

            string expectedErrorMessage =
                "Virtualization service error ocurred, contact support.";

            IQueryable<object> someData = null;

            RenderFragment<object> someChildContent =
                CreateRenderFragment(typeof(SomeComponent<object>));

            var componentParameters = new ComponentParameter[]
            {
                ComponentParameter.CreateParameter(
                    nameof(BVirtualizationComponent<object>.ChildContent),
                    someChildContent),

                ComponentParameter.CreateParameter(
                    nameof(BVirtualizationComponent<object>.DataSource),
                    someData)
            };

            // when
            this.renderedComponent =
                RenderComponent<BVirtualizationComponent<object>>(componentParameters);

            // then
            this.renderedComponent.Instance.State.Should().Be(expectedState);
            this.renderedComponent.Instance.ErrorMessage.Should().Be(expectedErrorMessage);
            this.renderedComponent.Instance.Label.Should().NotBeNull();
            this.renderedComponent.Instance.Label.Value.Should().Be(expectedErrorMessage);
        }
    }
}
