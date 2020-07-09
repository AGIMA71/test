using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ABNLookup.Dtos;
using ABNLookup.Interfaces;
using ABNLookup.Constants;
using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Http;
using ABNLookup.Validation;
using System.Collections.Generic;
using ABNLookup.CustomFilters;

namespace ABNLookup.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0", Deprecated =true)]
    [Route("api/v{version:apiVersion}/acn")]
    [ServiceFilter(typeof(DeprecatedFilterAttribute))]
    public class AcnController : BaseController
    {
        private readonly IAcnService _acnService;

        public AcnController(IAcnService acnService, IMessageCodeService messageCodeService, ISortMappingService sortMappingService)
            : base(messageCodeService, sortMappingService) =>
            _acnService = acnService;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<AcnV1DTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrors), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetAcnV1(string id, [FromQuery] string sort="")
        {
            var validationMessages = ValidateSortExpression<AcnV1DTO>(sort);
            if (validationMessages.Count > 0)
            {
                return ValidationErrorsResult(validationMessages);
            }

            var acn = await _acnService.GetAcnAsync<AcnV1DTO>(id,sort);

            return ErrorResult(acn) ?? Ok(acn);
        }

        [HttpGet]        
        [ProducesResponseType(typeof(ResourceLink), StatusCodes.Status200OK)]
        public ResourceLink GetAllAcnV1() =>
            _acnService.GetAcnResourceLinks(HttpContext,
                AbnLookupConstants.ApiVersion1,
                AbnLookupConstants.AcnV1Actions);
       
    }
}