// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Bunit;
using bVirtualization.Views.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tynamix.ObjectFiller;

namespace bVirtualization.Tests.Unit.Views.Components.BVirtualizations
{
    public partial class BVirtualizationComponentTests : TestContext
    {
        private IRenderedComponent<BVirtualizationComponent<object>> renderedComponent;
        private Mock<IQueryable<object>> dataSourceMock;

        public BVirtualizationComponentTests()
        {
            this.dataSourceMock = new Mock<IQueryable<object>>();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        public static IQueryable<object> CreateRandomQueryable() =>
            CreateQueryableFiller().Create().AsQueryable();

        private static object CreateRandomObject() =>
            new MnemonicString().GetValue();

        private static RenderFragment<object> CreateRenderFragment(Type type) =>
        context => builder =>
        {
            builder.OpenComponent(0, type);
            builder.CloseComponent();
        };

        public static Filler<List<object>> CreateQueryableFiller()
        {
            var filler = new Filler<List<object>>();

            filler.Setup()
                .OnType<object>().Use(() => CreateRandomObject());

            return filler;
        }
    }

    public class SomeComponent<T> : ComponentBase { }
}
