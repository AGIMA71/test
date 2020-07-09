using ABNLookup.Controllers;
using ABNLookup.Dtos;
using ABNLookup.Hypermedia;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ABNLookup.Constants;

namespace ABNLookupUnitTests.ControllerTests.Abn
{
    public class AbnV2 : AbnBase<AbnV2DTO>
    {
        private readonly AbnV2Controller systemUnderTest;
        public AbnV2()=>
            systemUnderTest = new AbnV2Controller(AbnService, messageCodeService, sortValidationService, loggerManager);       
        public async override Task<IActionResult> GetAbn(string id) => await systemUnderTest.GetAbnV2(id);
        public override ResourceLink GetAllAbn() => systemUnderTest.GetAllAbnV2();

        public async Task<IActionResult> RegisterAbn(AbnRegisterDTO abnRegisterDTO) => await systemUnderTest.Register(abnRegisterDTO);

        /// <summary>
        /// Return unprocessability (422) when business name already exists.
        /// </summary>
        /// <returns></returns>
        public async Task VerifyAbnUnProcessableEnittyForBusinessAlreadyRegisteredAsync()
        {
            var actual = await RegisterAbn(fakeAbnRegisterDTO);

            using (new AssertionScope())
            {
                ((UnprocessableEntityObjectResult)actual).StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
            }
        }

        /// <summary>
        /// Register a business
        /// </summary>
        /// <returns></returns>
        public async Task VerifyRegisterBusinessAsnyn()
        {
            var actual = await RegisterAbn(fakeAbnRegisterDTO);

            using (new AssertionScope())
            {
                ((ObjectResult) actual).Value.Should().BeEquivalentTo(fakeAbnNewDto);
            }
        }

        /// <summary>
        /// Verify 201 craeted status code.
        /// </summary>
        /// <returns></returns>
        public async Task VerifyRegisterBusinessStatusCodeAsnyn()
        {
            var actual = await RegisterAbn(fakeAbnRegisterDTO);

            using (new AssertionScope())
            {
                (((ObjectResult)actual)).StatusCode.Should().Be(StatusCodes.Status201Created);
            }
        }

        /// <summary>
        /// when modelstate is invalid
        /// then 400 BadRequest should be thrown.
        /// </summary>
        /// <returns></returns>
        public async Task Verify_WithInvalid_ModelState_Returns_BadRequestAsnyn()
        {
            systemUnderTest.ModelState.AddModelError("mykey", "Error");
            var actual = await RegisterAbn(fakeAbnRegisterDTO);

            using (new AssertionScope())
            {
                (((StatusCodeResult)actual)).StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            }
            systemUnderTest.ModelState.Remove("mykey");
        }

        /// <summary>
        /// validation failes for empty business name
        /// </summary>
        /// <returns></returns>
        public void Verify_EmptyBusinessName_Returns_Required_ErrorMessage()
        {
            var businessName = fakeAbnRegisterDTO.BusinessName;
           
            fakeAbnRegisterDTO.BusinessName = string.Empty;
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(fakeAbnRegisterDTO, new ValidationContext(fakeAbnRegisterDTO),validationResults,true);
            actual.Should().BeFalse();

            validationResults[0].ErrorMessage.Should().Be("The BusinessName field is required.");
            // reset business name
            fakeAbnRegisterDTO.BusinessName = businessName;
        }

        /// <summary>
        /// Business Name should be three character long.
        /// </summary>
        public void Verify_BusinessName_ShouldBe_Minimum_3CharactersLong()
        {
            var businessName = fakeAbnRegisterDTO.BusinessName;

            fakeAbnRegisterDTO.BusinessName = "ab";
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(fakeAbnRegisterDTO, new ValidationContext(fakeAbnRegisterDTO), validationResults, true);
            actual.Should().BeFalse();

            validationResults[0].ErrorMessage.Should().Be(AbnLookupConstants.Minium3CharactersLong);
            // reset business name
            fakeAbnRegisterDTO.BusinessName = businessName;
        }

        /// <summary>
        /// Max 50 characters allow3ed for a business name.
        /// </summary>
        public void Verify_Max_50_Characters_Allowed_For_BusinessName()
        {
            var businessName = fakeAbnRegisterDTO.BusinessName;
            // 51 characters long business name
            fakeAbnRegisterDTO.BusinessName = "This is new business this is new business this is n"; 
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(fakeAbnRegisterDTO, new ValidationContext(fakeAbnRegisterDTO), validationResults, true);
            actual.Should().BeFalse();

            validationResults[0].ErrorMessage.Should().Be(AbnLookupConstants.Max50CharactersAllowed);
            // reset business name
            fakeAbnRegisterDTO.BusinessName = businessName;
        }
        /// <summary>
        /// BusinessName with all digits fails validation.
        /// </summary>
        public void Verify_BusinessName_With_AllDitits_Fails_RegEx_Validation()
        {
            var businessName = fakeAbnRegisterDTO.BusinessName;
            // 51 characters long business name
            fakeAbnRegisterDTO.BusinessName = "123456789";
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(fakeAbnRegisterDTO, new ValidationContext(fakeAbnRegisterDTO), validationResults, true);
            actual.Should().BeFalse();

            validationResults[0].ErrorMessage.Should().Be(AbnLookupConstants.InvalidCharactersInBusinessName);
            // reset business name
            fakeAbnRegisterDTO.BusinessName = businessName;
        }
        /// <summary>
        /// Business Name with all special characters fails validation.
        /// </summary>
        public void Verify_BusinessName_With_AllSpecialCharacters_Fails_RegEx_Validation()
        {
            var businessName = fakeAbnRegisterDTO.BusinessName;
            // 51 characters long business name
            fakeAbnRegisterDTO.BusinessName = "@@@@@@@@@@";
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(fakeAbnRegisterDTO, new ValidationContext(fakeAbnRegisterDTO), validationResults, true);
            actual.Should().BeFalse();

            validationResults[0].ErrorMessage.Should().Be(AbnLookupConstants.OptionalSpecialCharactersMessage);
            // reset business name
            fakeAbnRegisterDTO.BusinessName = businessName;
        }

        /// <summary>
        /// Business Namw with % special character fails regex validation.
        /// </summary>
        public void Verify_BusinessName_With_NotPermitted_SpecialCharacters_Fails_RegEx_Validation()
        {
            var businessName = fakeAbnRegisterDTO.BusinessName;
            // 51 characters long business name
            fakeAbnRegisterDTO.BusinessName = "BusinessNameWith%";
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(fakeAbnRegisterDTO, new ValidationContext(fakeAbnRegisterDTO), validationResults, true);
            actual.Should().BeFalse();

            validationResults[0].ErrorMessage.Should().Be(AbnLookupConstants.InvalidCharactersInBusinessName);
            // reset business name
            fakeAbnRegisterDTO.BusinessName = businessName;
        }
        /// <summary>
        /// some special characters eg @ is permitted.
        /// </summary>
        public void Verify_BusinessName_With_Permitted_SpecialCharacters_Pass_RegEx_Validation()
        {
            var businessName = fakeAbnRegisterDTO.BusinessName;
            // 51 characters long business name
            fakeAbnRegisterDTO.BusinessName = "Communities@work";
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(fakeAbnRegisterDTO, new ValidationContext(fakeAbnRegisterDTO), validationResults, true);
            actual.Should().BeTrue();

            validationResults.Count.Should().Be(0);
            // reset business name
            fakeAbnRegisterDTO.BusinessName = businessName;
        }

        public void Verify_BusinessName_Can_Have_A_Digit_Character()
        {
            var businessName = fakeAbnRegisterDTO.BusinessName;
            // 51 characters long business name
            fakeAbnRegisterDTO.BusinessName = "2Guys Pty Ltd";
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(fakeAbnRegisterDTO, new ValidationContext(fakeAbnRegisterDTO), validationResults, true);
            actual.Should().BeTrue();

            validationResults.Count.Should().Be(0);
            // reset business name
            fakeAbnRegisterDTO.BusinessName = businessName;
        }

    }
}
