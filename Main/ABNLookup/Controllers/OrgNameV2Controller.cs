using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ABNLookup.Dtos;
using ABNLookup.Interfaces;
using ABNLookup.Validation;
using Microsoft.AspNetCore.Http;
using ABNLookup.Hypermedia;
using ABNLookup.Constants;

namespace ABNLookup.Controllers
{
    [Produces("application/json")]
    [ApiController] 
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/orgname")]
    public class OrgNameV2Controller : BaseController
    {
        private readonly IOrgNameService _orgNameService;

        public OrgNameV2Controller(IOrgNameService orgnameService, IMessageCodeService messageCodeService, ISortMappingService sortMappingService)
            : base(messageCodeService, sortMappingService)
        {
            _orgNameService = orgnameService;
        }

        [HttpGet("{name}")]      
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<AbnV2DTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrors), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetOrgNameV2(string name, [FromQuery] string sort = "")
        {
            if (name.Length < ValidationConstants.NameSearchLengthLimit)
                return SearchFieldTooShort();

            var validationMessages = ValidateSortExpression<AbnV2DTO>(sort);
            if (validationMessages.Count > 0)
            {
                return ValidationErrorsResult(validationMessages);
            }

            var orgs = await this._orgNameService.GetOrgAsync<AbnV2DTO>(name, sort);

            return ErrorResult(orgs) ?? Ok(orgs);
        }

        [HttpGet]        
        [ProducesResponseType(typeof(ResourceLink), StatusCodes.Status200OK)]
        public ResourceLink GetAllAbnV2() =>
            _orgNameService.GetOrgResourceLinks(HttpContext,
                AbnLookupConstants.ApiVersion2,
                AbnLookupConstants.OrgV2Actions);
    }
}