// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;
using bVirtualization.Brokers.DataSources;

namespace bVirtualization.Services
{
    public partial class VirtualizationService<T> : IVirtualizationService<T>
    {
        private readonly IDataSourceBroker<T> dataSourceBroker;

        public VirtualizationService(IDataSourceBroker<T> dataSourceBroker) =>
            this.dataSourceBroker = dataSourceBroker;

        public IQueryable<T> LoadFirstPage(uint startAt, uint pageSize) =>
        TryCatch(() => this.dataSourceBroker.TakeSkip(startAt, pageSize));
    }
}
