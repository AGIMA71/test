using ABNLookup.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABNLookupUnitTests.ControllerTests.OrgName
{
    [TestClass]
    public class OrgNameControllerTests : TestBase
    {
        public OrgNameControllerTests() : base()
        { }

        private OrgNameV1 OrgNameV1;
        private OrgNameV2 OrgNameV2;

        [TestCleanup]
        public void TestClean()
        {
            OrgNameV1 = null;
            OrgNameV2 = null;
        }

        [TestInitialize]
        public void TestInit()
        {
            OrgNameV1 = new OrgNameV1();
            OrgNameV2 = new OrgNameV2();
        }

        [TestMethod]
        public void GetAllOrgNamesAsyncV1_Should_ReturnSuccessfulResponse()
        {
            OrgNameV1.VerifyGetAllOrgNamesAsync();
        }

        [TestMethod]
        public void GetAllOrgNamesAsyncV2_Should_ReturnSuccessfulResponse()
        {
            OrgNameV2.VerifyGetAllOrgNamesAsync();
        }

        [TestMethod]
        public async Task GetOrgNameAsyncV1_Should_ReturnSuccessfulResponse()
        {
            await OrgNameV1.VerifyGetOrgNameAsync();
        }

        [TestMethod]
        public async Task GetOrgNameAsyncV2_Should_ReturnSuccessfulResponse()
        {
            await OrgNameV2.VerifyGetOrgNameAsync();
        }

        [TestMethod]
        public async Task GetOrgNameAsyncV1_WhenNoMatch_Should_ReturnNotFoundResponse()
        {
            // Arrange
            OrgNameV1.OrgNameService.GetOrgAsync<AbnV1DTO>(Arg.Any<string>()).Returns((IEnumerable<AbnV1DTO>)null);
            // Act
            await OrgNameV1.VerifyOrgNameNotFoundAsync();
            // restore setup
            OrgNameV1.OrgNameService.GetOrgAsync<AbnV1DTO>(Arg.Any<string>()).Returns(OrgNameV1.fakeGetOrgName);
        }
        [TestMethod]
        public async Task GetOrgNameAsyncV2_WhenNoMatch_Should_ReturnNotFoundResponse()
        {
            // Arrange
            OrgNameV2.OrgNameService.GetOrgAsync<AbnV2DTO>(Arg.Any<string>()).Returns((IEnumerable<AbnV2DTO>)null);
            // Act
            await OrgNameV2.VerifyOrgNameNotFoundAsync();
            // restore setup
            OrgNameV2.OrgNameService.GetOrgAsync<AbnV2DTO>(Arg.Any<string>()).Returns(OrgNameV2.fakeGetOrgName);
        }

        [TestMethod]
        public async Task GetAllOrgNamesV1Async_WhenSearchTooShort_Should_ReturnUnprocessableEntity()
        {
            await OrgNameV1.VerifyUnprocessableEntityAsync();            
        }

        [TestMethod]
        public async Task GetAllOrgNamesV2Async_WhenSearchTooShort_Should_ReturnUnprocessableEntity()
        {
            await OrgNameV2.VerifyUnprocessableEntityAsync();
        }

        [TestMethod]
        public async Task GetOrgNameAsyncV1_WhenMoreThan10Results_Should_ReturnUnProcessableEntityResponse()
        {
            // Arrange
            OrgNameV1.OrgNameService.GetOrgAsync<AbnV1DTO>(Arg.Any<string>()).Returns(OrgNameV1.fakeGetOrgNamesListMoreThan10);
            // Act
            await OrgNameV1.VerifyOrgNameUnProcessableEnittyForMoreThan10ResultsAsync();
            // restore setup
            OrgNameV1.OrgNameService.GetOrgAsync<AbnV1DTO>(Arg.Any<string>()).Returns(OrgNameV1.fakeGetOrgName);
        }

        [TestMethod]
        public async Task GetOrgNameAsyncV2_WhenMoreThan10Results_Should_ReturnUnProcessableEntityResponse()
        {
            // Arrange
            OrgNameV2.OrgNameService.GetOrgAsync<AbnV2DTO>(Arg.Any<string>()).Returns(OrgNameV2.fakeGetOrgNamesListMoreThan10);
            // Act
            await OrgNameV2.VerifyOrgNameUnProcessableEnittyForMoreThan10ResultsAsync();
            // restore setup
            OrgNameV2.OrgNameService.GetOrgAsync<AbnV2DTO>(Arg.Any<string>()).Returns(OrgNameV2.fakeGetOrgName);
        }


    }
}
