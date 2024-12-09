// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetDefaultProductKeyTest : DismTestBase
    {
        public GetDefaultProductKeyTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetDefaultKeyForOnlineSession()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                string productKey = DismApi.GetDefaultProductKey(session);

                productKey.ShouldNotBeNullOrWhiteSpace();
            }
        }
    }
}
