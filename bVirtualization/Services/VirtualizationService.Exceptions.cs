// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using bVirtualization.Models.Virtualizations.Exceptions;

namespace bVirtualization.Services
{
    public partial class VirtualizationService<T>
    {
        private delegate IQueryable<T> ReturningQueryableFunction();

        private IQueryable<T> TryCatch(ReturningQueryableFunction returningQueryableFunction)
        {
            try
            {
                return returningQueryableFunction();
            }
            catch (Exception exception)
            {
                var virtualizationServiceException =
                    new VirtualizationServiceException(exception);

                throw virtualizationServiceException;
            }
        }
    }
}
