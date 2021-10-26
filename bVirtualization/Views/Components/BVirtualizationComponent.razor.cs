// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Parameter]
        public DataSourceAsyncFunction DataSourceAsyncDelegate { get; set; }

        public BVirutalizationComponentState State { get; set; }
        public string ErrorMessage { get; set; }
        public LabelBase Label { get; set; }
        private IDataSourceBroker<T> dataSourceBroker;
        private IVirtualizationService<T> virtualizationService;

        protected override void OnInitialized()
        {
            //this.dataSourceBroker =
            //    new DataSourceBroker<T>(this.DataSource);

            //this.virtualizationService =
            //    new VirtualizationService<T>(this.dataSourceBroker);

            this.State = BVirutalizationComponentState.Content;
            base.OnInitialized();
        }

        private async ValueTask<(IReadOnlyList<T> DataSource, int TotalCount)> RetrieveDataAsync(
            int index,
            int quantity)
        {
            try
            {

                //IQueryable<T> data = this.virtualizationService
                //    .LoadPage((uint)index, (uint)quantity);

                var dataList =
                    await this.DataSourceAsyncDelegate(index, quantity);

                int totalCount = dataList.Item2;

                return (dataList.Item1, totalCount);

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

        public delegate ValueTask<(IReadOnlyList<T>, int)> DataSourceAsyncFunction(
            int startIndex,
            int totalCount);
    }
}