using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABNLookup.Dtos;
using ABNLookup.Hypermedia;
using ABNLookup.Interfaces;
using ABNLookup.Constants;
using ABNLookup.Validation;
using ABNLookup.Infrastructure.Logging;
using ABNLookup.Extensions;
using ABNLookup.Filters;

namespace ABNLookup.Controllers
{
    [Produces("application/json")]
    [ApiController]   
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/abn")]
    [ServiceFilter(typeof(ValidateModelAttribute))]
    public class AbnV2Controller : BaseController
    {
        private readonly IAbnService _abnService;
        private readonly ILoggerManager _logger;

        public AbnV2Controller(IAbnService abnService, IMessageCodeService messageCodeService, ISortMappingService sortMappingService, ILoggerManager logger)
            : base(messageCodeService, sortMappingService)
        {
            _abnService = abnService;
            _logger = logger;
        }
        
        [HttpGet("{id}",Name = "GetAbnV2")]        
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<AbnV2DTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrors), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetAbnV2(string id, [FromQuery] string sort = "")
        {
            var validationMessages = ValidateSortExpression<AbnV2DTO>(sort);
            if (validationMessages.Count > 0)
            {
                return ValidationErrorsResult(validationMessages);
            }

            var abn = await _abnService.GetAbnAsync<AbnV2DTO>(id, sort);

            return ErrorResult(abn) ?? Ok(abn);
        }

        [HttpGet]        
        [ProducesResponseType(typeof(ResourceLink), StatusCodes.Status200OK)]
        public ResourceLink GetAllAbnV2() =>
            _abnService.GetAbnResourceLinks(HttpContext,
                AbnLookupConstants.ApiVersion2,
                AbnLookupConstants.AbnV2Actions);

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<AbnNewDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProcessMessage), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Register(AbnRegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                model.BusinessName = model.BusinessName.RemoveAdditonalSpaces();

                if (await _abnService.BusniessNameAlreadyRegistered(model.BusinessName))
                    return CannotRegisterBusiness();

                var abn = await  _abnService.Register(model);
                if (abn != null)
                {                    
                    return CreatedAtAction(nameof(GetAbnV2), AbnLookupConstants.AbnV2Controller, new { version = AbnLookupConstants.ApiVersion2, id = abn.AustralianBusinessNumber }, abn);                    
                }
                return null;
            }
            
            return BadRequest();
        }
    }
}