// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;

namespace bVirtualization.Brokers.DataSources
{
    public class DataSourceBroker<T> : IDataSourceBroker<T>
    {
        private readonly IQueryable<T> dataSource;

        public DataSourceBroker(IQueryable<T> dataSource) =>
            this.dataSource = dataSource;

        public IQueryable<T> TakeSkip(uint startAt, uint pageSize) =>
            this.dataSource.Skip((int)startAt).Take((int)pageSize);
    }
}
