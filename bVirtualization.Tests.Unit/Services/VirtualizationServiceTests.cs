// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using bVirtualization.Brokers.DataSources;
using bVirtualization.Services;
using Moq;
using Tynamix.ObjectFiller;

namespace bVirtualization.Tests.Unit.Services
{
    public partial class VirtualizationServiceTests
    {

        private readonly Mock<IDataSourceBroker<object>> dataSourceBrokerMock;
        private readonly IVirtualizationService<object> virtualizationService;

        public VirtualizationServiceTests()
        {
            this.dataSourceBrokerMock = new Mock<IDataSourceBroker<object>>();

            this.virtualizationService = new VirtualizationService<object>(
                dataSourceBroker: this.dataSourceBrokerMock.Object);
        }

        private static uint GetRandomPositiveNumber() =>
            (uint)new IntRange(min: 0, max: 10).GetValue();

        private static string GetRandomMessage() =>
            new MnemonicString().GetValue();

        public static IQueryable<object> CreateRandomQueryable() =>
            CreateQueryableFiller().Create().AsQueryable();

        private static object CreateRandomObject() =>
            new MnemonicString().GetValue();

        public static Filler<List<object>> CreateQueryableFiller()
        {
            var filler = new Filler<List<object>>();

            filler.Setup()
                .OnType<object>().Use(CreateRandomObject());

            return filler;
        }
    }
}
