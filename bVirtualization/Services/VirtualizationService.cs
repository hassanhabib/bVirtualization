// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using bVirtualization.Brokers.DataSources;

namespace bVirtualization.Services
{
    public partial class VirtualizationService<T> : IVirtualizationService<T>
    {
        private readonly IDataSourceBroker<T> dataSourceBroker;
        private uint currentPageSize;

        public VirtualizationService(IDataSourceBroker<T> dataSourceBroker) =>
            this.dataSourceBroker = dataSourceBroker;

        public uint GetPageSize() =>
            this.currentPageSize;

        public IQueryable<T> LoadFirstPage(uint startAt, uint pageSize) =>
        TryCatch(() =>
        {
            this.currentPageSize = pageSize;

            return this.dataSourceBroker.TakeSkip(startAt, pageSize);
        });

        public uint GetCurrentPosition()
        {
            throw new NotImplementedException();
        }
    }
}
