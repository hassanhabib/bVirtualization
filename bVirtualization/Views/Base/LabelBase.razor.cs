// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace bVirtualization.Views.Base
{
    public partial class LabelBase
    {
        [Parameter]
        public string Value { get; set; }

        public void SetValue(string value)
        {
            this.Value = value;
            InvokeAsync(workItem: StateHasChanged);
        }
    }
}