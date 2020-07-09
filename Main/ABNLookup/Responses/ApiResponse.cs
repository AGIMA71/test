using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ABNLookup.Validation;

namespace ABNLookup
{
    public static class ApiResponse
    {
        public static IActionResult BadRequest(ProcessMessage processMessage) =>
           new BadRequestObjectResult(ValidationErrors.SingleError(processMessage));

        public static IActionResult BadRequest(IList<ProcessMessage> processMessages) =>
           new BadRequestObjectResult(ValidationErrors.MultipleErrors(processMessages));

        public static IActionResult UnprocessableEntity(ProcessMessage processMessage) =>
           new UnprocessableEntityObjectResult(ValidationErrors.SingleError(processMessage));

        public static IActionResult UnprocessableEntity(IList<ProcessMessage> processMessages) =>
           new UnprocessableEntityObjectResult(ValidationErrors.MultipleErrors(processMessages));

        public static IActionResult OkDataResult<T>(T data) => new OkObjectResult(data);
    }
}