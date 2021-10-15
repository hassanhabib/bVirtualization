// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using bVirtualization.Models.BVirutalizationComponents;
using Microsoft.AspNetCore.Components;

namespace bVirtualization.Views.Components
{
    public partial class BVirtualizationComponent<T> : ComponentBase
    {
        [Parameter]
        //[EditorRequired]
        public RenderFragment<T> ChildContent { get; set; }

        [Parameter]
        //[EditorRequired]
        public IQueryable<T> DataSource { get; set; }

        public BVirutalizationComponentState State { get; set; }
        public string ErrorMessage { get; set; }

        private (IQueryable<T> DataSource, int TotalCount) RetrieveData(int index, int quantity)
        {
            return (DataSource.Skip(index).Take(quantity), DataSource.Count());
        }
    }
}