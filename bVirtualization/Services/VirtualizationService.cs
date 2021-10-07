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

        public uint GetCurrentPosition() =>
            this.currentPosition;

        public uint GetPageSize() =>
            this.currentPageSize;

        public IQueryable<T> RetrieveNextPage()
        {
            throw new System.NotImplementedException();
        }
    }
}
