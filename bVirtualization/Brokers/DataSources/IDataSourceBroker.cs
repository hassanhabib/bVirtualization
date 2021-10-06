// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;

namespace bVirtualization.Brokers.DataSources
{
    public interface IDataSourceBroker<T>
    {
        public IQueryable<T> TakeSkip(uint startAt, uint pageSize);
    }
}
