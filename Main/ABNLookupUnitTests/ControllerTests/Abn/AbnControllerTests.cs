using ABNLookup.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABNLookupUnitTests.ControllerTests.Abn
{
    [TestClass]
    public class AbnControllerTests : TestBase
    {
        public AbnControllerTests() : base()
        { }

        private AbnV1 Abnv1;
        private AbnV2 Abnv2;

        [TestCleanup]
        public void TestClean()
        {
            Abnv1 = null;
            Abnv2 = null;
        }

        [TestInitialize]
        public void TestInit()
        {
            Abnv1 = new AbnV1();
            Abnv2 = new AbnV2();
        }

        [TestMethod]
        public void GetAllAbnAsyncV1_Should_ReturnSuccessfulResponse()
        {
            Abnv1.VerifyGetAllAbnAsync();
        }

        [TestMethod]
        public void GetAllAbnAsyncV2_Should_ReturnSuccessfulResponse()
        {
            Abnv2.VerifyGetAllAbnAsync();
        }

        [TestMethod]
        public async Task GetAbnAsyncV1_Should_ReturnSuccessfulResponse()
        {
            await Abnv1.VerifyGetAbnAsync();
        }

        [TestMethod]
        public async Task GetAbnAsyncV2_Should_ReturnSuccessfulResponse()
        {
            await Abnv2.VerifyGetAbnAsync();
        }

        [TestMethod]
        public async Task GetAbnAsyncV1_WhenNoMatch_Should_ReturnNotFoundResponse()
        {
            // Arrange
            Abnv1.AbnService.GetAbnAsync<AbnV1DTO>(Arg.Any<string>()).Returns((IEnumerable<AbnV1DTO>)null);
            // Act
            await Abnv1.VerifyAbnNotFoundAsync();
            // restore setup
            Abnv1.AbnService.GetAbnAsync<AbnV1DTO>(Arg.Any<string>()).Returns(Abnv1.fakeGetAbn);
        }
        [TestMethod]
        public async Task GetAbnAsyncV2_WhenNoMatch_Should_ReturnNotFoundResponse()
        {
            // Arrange
            Abnv2.AbnService.GetAbnAsync<AbnV2DTO>(Arg.Any<string>()).Returns((IEnumerable<AbnV2DTO>)null);
            // Act
            await Abnv2.VerifyAbnNotFoundAsync();
            // restore setup
            Abnv2.AbnService.GetAbnAsync<AbnV2DTO>(Arg.Any<string>()).Returns(Abnv2.fakeGetAbn);
        }

        [TestMethod]
        public async Task GetAbnAsyncV1_WhenMoreThan10Results_Should_ReturnUnProcessableEntityResponse()
        {
            // Arrange
            Abnv1.AbnService.GetAbnAsync<AbnV1DTO>(Arg.Any<string>()).Returns(Abnv1.fakeGetAbnListMoreThan10);
            // Act
            await Abnv1.VerifyAbnUnProcessableEnittyForMoreThan10ResultsAsync();
            // restore setup
            Abnv1.AbnService.GetAbnAsync<AbnV1DTO>(Arg.Any<string>()).Returns(Abnv1.fakeGetAbn);
        }

        [TestMethod]
        public async Task GetAbnAsyncV2_WhenMoreThan10Results_Should_ReturnUnProcessableEntityResponse()
        {
            // Arrange
            Abnv2.AbnService.GetAbnAsync<AbnV2DTO>(Arg.Any<string>()).Returns(Abnv2.fakeGetAbnListMoreThan10);
            // Act
            await Abnv2.VerifyAbnUnProcessableEnittyForMoreThan10ResultsAsync();
            // restore setup
            Abnv2.AbnService.GetAbnAsync<AbnV2DTO>(Arg.Any<string>()).Returns(Abnv2.fakeGetAbn);
        }

        [TestMethod]
        public async Task BusinessName_Already_Registered_Should__ReturnUnProcessableEntityResponse()
        {
            // Arrange
            Abnv2.AbnService.BusniessNameAlreadyRegistered(Arg.Any<string>()).Returns(true);
            // Act
            await Abnv2.VerifyAbnUnProcessableEnittyForBusinessAlreadyRegisteredAsync();
            // restore setup
            Abnv2.AbnService.BusniessNameAlreadyRegistered(Arg.Any<string>()).Returns(false);
        }

        [TestMethod]
        public async Task Register_NewBusiness_Should_ReturnSuccessfulResponse()
        {
            await Abnv2.VerifyRegisterBusinessAsnyn();
        }

        [TestMethod]
        public async Task Register_NewBusiness_Should_ReturnSuccessful_201CraetedResponse()
        {
            await Abnv2.VerifyRegisterBusinessStatusCodeAsnyn();
        }
        [TestMethod]
        public async Task Register_NewBusiness_WithInvalid_ModelState_Should_ReturnBadResponse()
        {
            await Abnv2.Verify_WithInvalid_ModelState_Returns_BadRequestAsnyn();
        }
        [TestMethod]
        public void Register_EmptyBusinessName_Should_Return_Required_ErrorMessage()
        {
           Abnv2.Verify_EmptyBusinessName_Returns_Required_ErrorMessage();
        }
        [TestMethod]
        public void Register_Business_Name_ShouldBe_Min_Three_CharactersLong()
        {
            Abnv2.Verify_BusinessName_ShouldBe_Minimum_3CharactersLong();
        }
        [TestMethod]
        public void Register_Business_Name_ShouldBe_Max_Fifty_CharactersAllowed()
        {
            Abnv2.Verify_Max_50_Characters_Allowed_For_BusinessName();
        }
        [TestMethod]
        public void Register_Business_Name_With_AllDigits_Returns_Validation_ErrorMessage()
        {
            Abnv2.Verify_BusinessName_With_AllDitits_Fails_RegEx_Validation();
        }
        [TestMethod]
        public void Register_Business_Name_With_AllSpecialCharacters_Returns_Validation_ErrorMessage()
        {
            Abnv2.Verify_BusinessName_With_AllSpecialCharacters_Fails_RegEx_Validation();
        }
        [TestMethod]
        public void Register_Business_Name_With_NotPermitted_PercentageCharacter_Returns_Validation_ErrorMessage()
        {
            Abnv2.Verify_BusinessName_With_NotPermitted_SpecialCharacters_Fails_RegEx_Validation();
        }

        [TestMethod]
        public void Register_Business_Name_With_Permitted_At_Character_Returns_No_ErrorMessage()
        {
            Abnv2.Verify_BusinessName_With_Permitted_SpecialCharacters_Pass_RegEx_Validation();
        }
        [TestMethod]
        public void Register_Business_Name_Can_Have_Digits()
        {
            Abnv2.Verify_BusinessName_Can_Have_A_Digit_Character();
        }
    }
}
