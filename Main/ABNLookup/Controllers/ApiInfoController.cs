using ABNLookup.CustomFilters;
using ABNLookup.Domain.Models;
using ABNLookup.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABNLookup.Controllers
{
    [ApiController]
    [ApiVersion("1.0", Deprecated =true)]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}")]
    [ServiceFilter(typeof(DeprecatedFilterAttribute))]
    public class ApiInfoController : ControllerBase
    {
        private readonly IApiInfoService _apiInfoService;
        public ApiInfoController(IApiInfoService apiInfoService)
        {
           _apiInfoService = apiInfoService;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ApiInfo), StatusCodes.Status200OK)]
        public ApiInfo GetApiInfo() =>
           _apiInfoService.GetApiInfo();

        [HttpGet]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(ApiInfo), StatusCodes.Status200OK)]
        public ApiInfo GetApiV2Info() =>
          _apiInfoService.GetApiV2Info();

    }
}