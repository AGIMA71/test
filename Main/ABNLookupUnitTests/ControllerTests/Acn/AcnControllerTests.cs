using ABNLookup.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABNLookupUnitTests.ControllerTests.Acn
{
    [TestClass]
    public class AcnControllerTests:TestBase
    {
        public AcnControllerTests():base()
        { }

        private AcnV1 acnv1;
        private AcnV2 acnv2;

        [TestCleanup]
        public void TestClean()
        {
            acnv1 = null;
            acnv2 = null;
        }

        [TestInitialize]   
        public void TestInit()
        {
            acnv1 = new AcnV1();
            acnv2 = new AcnV2();
        }
    
        [TestMethod]
        public void GetAllAcnAsyncV1_Should_ReturnSuccessfulResponse()
        {
            acnv1.VerifyGetAllAcnAsync();            
        }

        [TestMethod]
        public void GetAllAcnAsyncV2_Should_ReturnSuccessfulResponse()
        {
            acnv2.VerifyGetAllAcnAsync();
        }

        [TestMethod]
        public async Task GetAcnAsyncV1_Should_ReturnSuccessfulResponse()
        {
            await acnv1.VerifyGetAcnAsync();                        
        }

        [TestMethod]
        public async Task GetAcnAsyncV2_Should_ReturnSuccessfulResponse()
        {
            await acnv2.VerifyGetAcnAsync();            
        }

        [TestMethod]
        public async Task GetAcnAsyncV1_WhenNoMatch_Should_ReturnNotFoundResponse()
        {
            // Arrange
            acnv1.acnService.GetAcnAsync<AcnV1DTO>(Arg.Any<string>()).Returns((IEnumerable<AcnV1DTO>)null);
            // Act
            await acnv1.VerifyAcnNotFoundAsync();
            // restore setup
            acnv1.acnService.GetAcnAsync<AcnV1DTO>(Arg.Any<string>()).Returns(acnv1.fakeGetAcn);           
        }
        [TestMethod]
        public async Task GetAcnAsyncV2_WhenNoMatch_Should_ReturnNotFoundResponse()
        {
            // Arrange
            acnv2.acnService.GetAcnAsync<AcnV2DTO>(Arg.Any<string>()).Returns((IEnumerable<AcnV2DTO>)null);
            // Act
            await acnv2.VerifyAcnNotFoundAsync();
            // restore setup
            acnv2.acnService.GetAcnAsync<AcnV2DTO>(Arg.Any<string>()).Returns(acnv2.fakeGetAcn);
        }

        [TestMethod]
        public async Task GetAcnAsyncV1_WhenMoreThan10Results_Should_ReturnUnProcessableEntityResponse()
        {
            // Arrange
            acnv1.acnService.GetAcnAsync<AcnV1DTO>(Arg.Any<string>()).Returns(acnv1.fakeGetAcnListMoreThan10);
            // Act
            await acnv1.VerifyAcnUnProcessableEnittyForMoreThan10ResultsAsync();
            // restore setup
            acnv1.acnService.GetAcnAsync<AcnV1DTO>(Arg.Any<string>()).Returns(acnv1.fakeGetAcn);
        }

        [TestMethod]
        public async Task GetAcnAsyncV2_WhenMoreThan10Results_Should_ReturnUnProcessableEntityResponse()
        {
            // Arrange
            acnv2.acnService.GetAcnAsync<AcnV2DTO>(Arg.Any<string>()).Returns(acnv2.fakeGetAcnListMoreThan10);
            // Act
            await acnv2.VerifyAcnUnProcessableEnittyForMoreThan10ResultsAsync();
            // restore setup
            acnv2.acnService.GetAcnAsync<AcnV2DTO>(Arg.Any<string>()).Returns(acnv2.fakeGetAcn);
        }
    }
}
