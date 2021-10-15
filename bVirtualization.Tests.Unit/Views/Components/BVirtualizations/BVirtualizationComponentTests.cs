// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Bunit;
using bVirtualization.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tynamix.ObjectFiller;
using bVirtualization.Views.Components;

namespace bVirtualization.Tests.Unit.Views.Components.BVirtualizations
{
    public partial class BVirtualizationComponentTests : TestContext
    {
        private readonly Mock<IVirtualizationService<object>> virtualizationServiceMock;
        private IRenderedComponent<BVirtualizationComponent<object>> renderedComponent;

        public BVirtualizationComponentTests()
        {
            this.virtualizationServiceMock =
                new Mock<IVirtualizationService<object>>();

            this.Services.AddTransient<IVirtualizationService<object>>(service =>
                this.virtualizationServiceMock.Object);

            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        public static IQueryable<object> CreateRandomQueryable() =>
            CreateQueryableFiller().Create().AsQueryable();

        private static object CreateRandomObject() =>
            new MnemonicString().GetValue();

        public static Filler<List<object>> CreateQueryableFiller()
        {
            var filler = new Filler<List<object>>();

            filler.Setup()
                .OnType<object>().Use(CreateRandomObject());

            return filler;
        }
    }
}
