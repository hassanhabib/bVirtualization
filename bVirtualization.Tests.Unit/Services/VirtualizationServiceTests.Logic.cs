﻿// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;
using FluentAssertions;
using Moq;
using Xunit;

namespace bVirtualization.Tests.Unit.Services
{
    public partial class VirtualizationServiceTests
    {
        [Fact]
        public void ShouldSkipAndTakeOnLoadFirstPageFromDataSource()
        {
            // given
            uint randomStartAt = GetRandomPositiveNumber();
            uint randomPageSize = GetRandomPositiveNumber();
            uint inputStartAt = randomStartAt;
            uint inputPageSize = randomPageSize;
            uint expectedPageSize = inputPageSize;
            uint expectedPosition = inputStartAt;

            IQueryable<object> randomQueryable =
                CreateRandomQueryable();

            IQueryable<object> returnedQueryable = randomQueryable;
            IQueryable<object> expectedQueryable = returnedQueryable;

            this.dataSourceBrokerMock.Setup(source =>
                source.TakeSkip(inputStartAt, inputPageSize))
                    .Returns(returnedQueryable);

            // when
            IQueryable<object> actualQueryable =
                this.virtualizationService.LoadFirstPage(
                    inputStartAt,
                    inputPageSize);

            uint actualPageSize =
                this.virtualizationService.GetPageSize();

            uint actualPosition =
                this.virtualizationService.GetCurrentPosition();

            // then
            actualQueryable.Should().BeEquivalentTo(expectedQueryable);
            actualPageSize.Should().Be(expectedPageSize);
            actualPosition.Should().Be(expectedPosition);

            this.dataSourceBrokerMock.Verify(source =>
                source.TakeSkip(inputStartAt, inputPageSize),
                    Times.Once);

            this.dataSourceBrokerMock.VerifyNoOtherCalls();
        }
    }
}
