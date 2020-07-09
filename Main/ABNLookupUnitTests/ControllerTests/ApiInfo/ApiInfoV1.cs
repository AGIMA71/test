namespace ABNLookupUnitTests.ControllerTests.ApiInfo
{
    internal class ApiInfoV1 :ApiInfoBase
    {
        public override ABNLookup.Domain.Models.ApiInfo GetApiInfo() => systemUnderTest.GetApiInfo();
    }
}
