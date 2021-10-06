// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using bVirtualization.Brokers.DataSources;
using bVirtualization.Models.Virtualizations.Exceptions;

namespace bVirtualization.Services
{
    public class VirtualizationService<T> : IVirtualizationService<T>
    {
        private readonly IDataSourceBroker<T> dataSourceBroker;

        public VirtualizationService(IDataSourceBroker<T> dataSourceBroker) =>
            this.dataSourceBroker = dataSourceBroker;

        public IQueryable<T> LoadFirstPage(uint startAt, uint pageSize)
        {
            try
            {
                return this.dataSourceBroker.TakeSkip(startAt, pageSize);
            }
            catch (Exception ex)
            {
                VirtualizationServiceException virtualizationServiceException =
                    new VirtualizationServiceException(ex);
                throw virtualizationServiceException;
            }
        }
    }
}
