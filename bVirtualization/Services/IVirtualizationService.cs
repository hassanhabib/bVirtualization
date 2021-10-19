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
        IQueryable<T> LoadPage(uint startAt, uint pageSize);
        IQueryable<T> RetrieveNextPage();
        uint GetCurrentPosition();
        uint GetPageSize();
    }
}
