// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace bVirtualization.Views.Base
{
    public partial class VirtualizationBase<T> : ComponentBase
    {
        [Parameter]
        public RenderFragment<T> ChildComponent { get; set; }

        [Parameter]
        public int OverscanCount { get; set; }

        [Parameter]
        public int Offset { get; set; }

        [Parameter]
        public int PageSize { get; set; }

        [Parameter]
        public Func<int, int, (IQueryable<T> DataSource, int TotalCount)> CallBackSource { get; set; }

        private async ValueTask<ItemsProviderResult<T>> GetItemsAsync(
            ItemsProviderRequest request)
        {
            var data = CallBackSource.Invoke(request.StartIndex, request.Count);

            return new ItemsProviderResult<T>(data.DataSource, data.TotalCount);
        }
    }
}
