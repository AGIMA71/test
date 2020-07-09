using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ABNLookup.Dtos;
using ABNLookup.Interfaces;
using ABNLookup.Validation;
using Microsoft.AspNetCore.Http;
using ABNLookup.Hypermedia;
using ABNLookup.Constants;
using ABNLookup.CustomFilters;

namespace ABNLookup.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0", Deprecated =true)]
    [Route("api/v{version:apiVersion}/orgname")]
    [ServiceFilter(typeof(DeprecatedFilterAttribute))]
    public class OrgNameController : BaseController
    {
        private readonly IOrgNameService _orgNameService;

        public OrgNameController(IOrgNameService orgnameService, IMessageCodeService messageCodeService, ISortMappingService sortMappingService)
            : base(messageCodeService, sortMappingService)
        {
            _orgNameService = orgnameService;
        }

        [HttpGet("{name}")]      
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<AbnV1DTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrors), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetOrgName(string name, [FromQuery] string sort="")
        {
            if (name.Length<ValidationConstants.NameSearchLengthLimit)
                return SearchFieldTooShort();

            var validationMessages = ValidateSortExpression<AbnV1DTO>(sort);
            if (validationMessages.Count > 0)
            {
                return ValidationErrorsResult(validationMessages);
            }

            var orgs = await _orgNameService.GetOrgAsync<AbnV1DTO>(name, sort);

            return ErrorResult(orgs) ?? Ok(orgs);
        }

        [HttpGet]      
        [ProducesResponseType(typeof(ResourceLink), StatusCodes.Status200OK)]
        public ResourceLink GetAllAbn() =>
            _orgNameService.GetOrgResourceLinks(HttpContext,
                AbnLookupConstants.ApiVersion1,
                AbnLookupConstants.OrgV1Actions);       
    }
}