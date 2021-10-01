// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;

namespace bVirtualization.Services
{
    public interface IVirtualizationService<T>
    {
        IQueryable<T> LoadFirstPage(int startAt, int pageSize);
    }
}
