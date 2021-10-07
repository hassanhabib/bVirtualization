// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using bVirtualization.Models.Virtualizations.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace bVirtualization.Tests.Unit.Services
{
    public partial class VirtualizationServiceTests
    {
        [Fact]
        public void ShouldThrowServiceExceptionOnTakeSkipIfServiceErrorOccurs()
        {
            // given
            uint someStartAt = GetRandomPositiveNumber();
            uint somePageSize = GetRandomPositiveNumber();
            string randomMessage = GetRandomMessage();
            var serviceException = new Exception(randomMessage);

            var expectedVirtualizationServiceException =
                new VirtualizationServiceException(serviceException);

            this.dataSourceBrokerMock.Setup(broker =>
                broker.TakeSkip(It.IsAny<uint>(), It.IsAny<uint>()))
                    .Throws(serviceException);

            // when
            Action takeSkipAction = () =>
                this.virtualizationService.LoadFirstPage(
                    someStartAt,
                    somePageSize);

            // then
            Assert.Throws<VirtualizationServiceException>(takeSkipAction);

            this.dataSourceBrokerMock.Verify(broker =>
                broker.TakeSkip(It.IsAny<uint>(), It.IsAny<uint>()),
                    Times.Once());

            this.dataSourceBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetreiveNextPageIfServiceErrorOccurs()
        {
            string randomMessage = GetRandomMessage();
            var serviceException = new Exception(randomMessage);

            var expectedVirtualizationServiceException =
                new VirtualizationServiceException(serviceException);

            this.dataSourceBrokerMock.Setup(broker =>
                broker.TakeSkip(It.IsAny<uint>(), It.IsAny<uint>()))
                    .Throws(serviceException);

            // when
            Action takeSkipAction = () =>
                this.virtualizationService.RetrieveNextPage();

            // then
            Assert.Throws<VirtualizationServiceException>(takeSkipAction);

            this.dataSourceBrokerMock.Verify(broker =>
                broker.TakeSkip(It.IsAny<uint>(), It.IsAny<uint>()),
                    Times.Once());

            this.dataSourceBrokerMock.VerifyNoOtherCalls();
        }
    }
}
