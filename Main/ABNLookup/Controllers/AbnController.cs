using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ABNLookup.Dtos;
using ABNLookup.Hypermedia;
using ABNLookup.Interfaces;
using ABNLookup.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ABNLookup.Validation;
using ABNLookup.Infrastructure.Logging;
using ABNLookup.CustomFilters;
using ABNLookup.Filters;

namespace ABNLookup.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0", Deprecated =true)]
    [Route("api/v{version:apiVersion}/abn")]
    [ServiceFilter(typeof(DeprecatedFilterAttribute))]
    [ServiceFilter(typeof(ValidateModelAttribute))]
    public class AbnController : BaseController
    {
        private readonly IAbnService _abnService;
        private readonly ILoggerManager _logger;

        public AbnController(IAbnService abnService, IMessageCodeService messageCodeService, ISortMappingService sortMappingService, ILoggerManager logger)
            : base(messageCodeService, sortMappingService)
        {
            _abnService = abnService;
            _logger = logger;
        }

        [HttpGet("{id}")]       
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<AbnV1DTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrors), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetAbnV1(string id, [FromQuery] string sort="")
        {
            var validationMessages = ValidateSortExpression<AbnV1DTO>(sort);
            if (validationMessages.Count > 0)
            {
                // Note: Temp to test logging to file.
                _logger.LogInfo($"Supplied sort parameters were incorrect - {validationMessages.Count}");
                return ValidationErrorsResult(validationMessages);
            }

            var abn = await _abnService.GetAbnAsync<AbnV1DTO>(id, sort);

            return ErrorResult(abn) ?? Ok(abn);
        }

        [HttpGet]        
        [ProducesResponseType(typeof(IEnumerable<AbnV1DTO>), StatusCodes.Status200OK)]
        public ResourceLink GetAllAbnV1() =>
            _abnService.GetAbnResourceLinks(HttpContext,
                AbnLookupConstants.ApiVersion1,
                AbnLookupConstants.AbnV1Actions);

    }
}