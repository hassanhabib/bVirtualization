// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;

namespace bVirtualization.Services
{
    public interface IVirtualizationService<T>
    {
        IQueryable<T> LoadFirstPage(uint startAt, uint pageSize);
        uint GetPageSize();
        uint GetCurrentPosition();
    }
}
