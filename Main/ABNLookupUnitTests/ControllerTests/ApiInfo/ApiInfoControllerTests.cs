using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ABNLookupUnitTests.ControllerTests.ApiInfo
{
    [TestClass]
    public class ApiInfoControllerTests : TestBase
    {
        public ApiInfoControllerTests():base()
        {
        }

        private ApiInfoV1 apiv1;
        private ApiInfoV2 apiv2;
        
        [TestCleanup]
        public void TestClean()
        {
            apiv1 = null;
            apiv2 = null;
        }

        [TestInitialize]
        public void TestInit()
        {
            apiv1 = new ApiInfoV1();
            apiv2 = new ApiInfoV2();
        }

        [TestMethod]
        public void GetApiInfoV1_Should_ReturnSuccessfulResponse()
        {
            apiv1.VerifyGetApiInfo();
        }

        [TestMethod]
        public void GetApiInfoV2_Should_ReturnSuccessfulResponse()
        {
            apiv2.VerifyGetApiInfo();
        }

        [TestMethod]
        public void GetApiInfoV1_Should_ReturnSuccessfulVersionResponse()
        {
            apiv1.VerifyApiVersion();
        }

        [TestMethod]
        public void GetApiInfoV2_Should_ReturnSuccessfulVersionResponse()
        {
            apiv2.VerifyApiVersion();
        }
        [TestMethod]
        public void GetApiInfoV1_Should_ReturnSuccessfulStatusResponse()
        {
            apiv1.VerifyApiStatus();
        }

        [TestMethod]
        public void GetApiInfoV2_Should_ReturnSuccessfulStatusResponse()
        {
            apiv2.VerifyApiStatus();
        }
    }
}
