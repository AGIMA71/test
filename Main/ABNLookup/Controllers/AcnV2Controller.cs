using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ABNLookup.Dtos;
using ABNLookup.Interfaces;
using ABNLookup.Constants;
using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Http;
using ABNLookup.Validation;
using System.Collections.Generic;

namespace ABNLookup.Controllers
{
    [Produces("application/json")]
    [ApiController]    
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/acn")]
    public class AcnV2Controller : BaseController
    {
        private readonly IAcnService _acnService;

        public AcnV2Controller(IAcnService acnService, IMessageCodeService messageCodeService, ISortMappingService sortMappingService)
            : base(messageCodeService, sortMappingService) =>
            _acnService = acnService;       

        [HttpGet("{id}")]       
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<AcnV2DTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrors), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetAcnV2(string id, [FromQuery] string sort = "")
        {
            var validationMessages = ValidateSortExpression<AcnV2DTO>(sort);
            if (validationMessages.Count > 0)
            {
                return ValidationErrorsResult(validationMessages);
            }

            var acn = await _acnService.GetAcnAsync<AcnV2DTO>(id, sort);

            return ErrorResult(acn) ?? Ok(acn);
        }

        [HttpGet]      
        [ProducesResponseType(typeof(ResourceLink), StatusCodes.Status200OK)]
        public ResourceLink GetAllAcnV2() =>
            _acnService.GetAcnResourceLinks(HttpContext,
                AbnLookupConstants.ApiVersion2,
                AbnLookupConstants.AcnV2Actions);
    }
}