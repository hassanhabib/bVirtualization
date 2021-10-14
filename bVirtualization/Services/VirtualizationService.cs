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
        private uint currentPageSize;
        private uint currentPosition;

        public VirtualizationService(IDataSourceBroker<T> dataSourceBroker) =>
            this.dataSourceBroker = dataSourceBroker;

        public IQueryable<T> LoadFirstPage(uint startAt, uint pageSize) =>
        TryCatch(() =>
        {
            this.currentPosition = startAt;
            this.currentPageSize = pageSize;

            return this.dataSourceBroker.TakeSkip(startAt, pageSize);
        });

        public IQueryable<T> RetrieveNextPage() =>
        TryCatch(() =>
        {
            this.currentPosition += this.currentPageSize;

            return this.dataSourceBroker.TakeSkip(
                this.currentPosition,
                this.currentPageSize);
        });

        public uint GetCurrentPosition() =>
            this.currentPosition;

        public uint GetPageSize() =>
            this.currentPageSize;
    }
}
