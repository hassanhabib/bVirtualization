// ---------------------------------------------------------------
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
                this.virtualizationService.LoadPage(
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

        [Fact]
        public void ShouldRetrieveNextPage()
        {
            // given
            uint randomStartAt = GetRandomPositiveNumber();
            uint randomPageSize = GetRandomPositiveNumber();
            uint inputStartAt = randomStartAt;
            uint inputPageSize = randomPageSize;
            uint expectedStartAt = inputStartAt + inputPageSize;
            uint expectedCurrentPosition = expectedStartAt;
            uint expectedPageSize = inputPageSize;

            IQueryable<object> retrievedNextPage =
                CreateRandomQueryable();

            IQueryable<object> expectedNextPage =
                retrievedNextPage;

            this.dataSourceBrokerMock.Setup(broker =>
                broker.TakeSkip(expectedStartAt, inputPageSize))
                    .Returns(retrievedNextPage);

            // when
            this.virtualizationService.LoadPage(
                inputStartAt,
                inputPageSize);

            IQueryable<object> actualNextPage =
                this.virtualizationService.RetrieveNextPage();

            uint actualCurrentPosition =
                this.virtualizationService.GetCurrentPosition();

            uint actualPageSize =
                this.virtualizationService.GetPageSize();

            // then
            actualNextPage.Should().BeEquivalentTo(expectedNextPage);
            actualCurrentPosition.Should().Be(expectedCurrentPosition);
            actualPageSize.Should().Be(expectedPageSize);

            this.dataSourceBrokerMock.Verify(broker =>
                broker.TakeSkip(inputStartAt, inputPageSize),
                    Times.Once);

            this.dataSourceBrokerMock.Verify(broker =>
                broker.TakeSkip(expectedStartAt, inputPageSize),
                    Times.Once);

            this.dataSourceBrokerMock.VerifyNoOtherCalls();
        }
    }
}
