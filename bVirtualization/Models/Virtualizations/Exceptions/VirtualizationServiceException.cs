// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace bVirtualization.Models.Virtualizations.Exceptions
{
    public class VirtualizationServiceException : Exception
    {
        public VirtualizationServiceException(Exception innerException)
            : base(message: "Virtualization service error ocurred, contact support.", innerException)
        { }
    }
}
