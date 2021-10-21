// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;
using bVirtualization.Brokers.DataSources;
using bVirtualization.Models.BVirutalizationComponents;
using bVirtualization.Services;
using bVirtualization.Views.Base;
using Microsoft.AspNetCore.Components;

namespace bVirtualization.Views.Components
{
    public partial class BVirtualizationComponent<T> : ComponentBase
    {
        [Parameter]
        public RenderFragment<T> ChildContent { get; set; }

        [Parameter]
        public IQueryable<T> DataSource { get; set; }

        public BVirutalizationComponentState State { get; set; }
        public string ErrorMessage { get; set; }
        public LabelBase Label { get; set; }
        private IDataSourceBroker<T> dataSourceBroker;
        public IVirtualizationService<T> VirtualizationService { get; set; }

        protected override void OnInitialized()
        {
            this.dataSourceBroker =
                new DataSourceBroker<T>(this.DataSource);

            this.VirtualizationService = this.VirtualizationService ??
                new VirtualizationService<T>(this.dataSourceBroker);

            this.State = BVirutalizationComponentState.Content;
            base.OnInitialized();
        }

        private (IQueryable<T> DataSource, int TotalCount) RetrieveData(
            int index,
            int quantity)
        {
            try
            {

                IQueryable<T> data = this.VirtualizationService
                    .LoadPage((uint)index, (uint)quantity);

                int totalCount = data.Count();

                return (data, totalCount);

            }
            catch (System.Exception exception)
            {
                this.State = BVirutalizationComponentState.Error;
                this.ErrorMessage = exception.Message;
                InvokeAsync(StateHasChanged);
                return default;
            }
        }

        public bool IsStateContent =>
            this.State == BVirutalizationComponentState.Content;

        public bool IsStateError =>
            this.State == BVirutalizationComponentState.Error;
    }
}