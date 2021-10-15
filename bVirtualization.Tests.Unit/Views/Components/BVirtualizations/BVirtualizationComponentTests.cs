// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Bunit;
using bVirtualization.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace bVirtualization.Tests.Unit.Views.Components.BVirtualizations
{
    public partial class BVirtualizationComponentTests : TestContext
    {
        private readonly Mock<IVirtualizationService<object>> virtualizationServiceMock;

        public BVirtualizationComponentTests()
        {
            this.virtualizationServiceMock =
                new Mock<IVirtualizationService<object>>();

            this.Services.AddTransient<IVirtualizationService<object>>(service =>
                this.virtualizationServiceMock.Object);
        }
    }
}
