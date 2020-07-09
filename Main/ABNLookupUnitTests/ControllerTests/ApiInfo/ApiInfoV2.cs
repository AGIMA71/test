using System;
using System.Collections.Generic;
using System.Text;

namespace ABNLookupUnitTests.ControllerTests.ApiInfo
{
    internal class ApiInfoV2 : ApiInfoBase
    {
        public override ABNLookup.Domain.Models.ApiInfo GetApiInfo() => systemUnderTest.GetApiV2Info();
    }
}
