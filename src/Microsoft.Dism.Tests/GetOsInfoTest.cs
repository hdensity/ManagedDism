// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetOsInfoTest : DismTestBase
    {
        public GetOsInfoTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetOnlineOsInfo()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                DismOsInfo osInfo = DismApi.GetOsInfo(session);

                osInfo.ShouldNotBeNull();
                osInfo.OsState.ShouldBe(DismOsState.Online);
                osInfo.WindowsDirectory.ShouldNotBeNullOrWhiteSpace();
                osInfo.BootDrive.ShouldNotBeNullOrWhiteSpace();
            }
        }
    }
}
