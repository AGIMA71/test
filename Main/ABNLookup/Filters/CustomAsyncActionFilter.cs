using ABNLookup.Extensions;
using ABNLookup.Interfaces;
using ABNLookup.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABNLookup.CustomFilters
{
    /// <summary>
    // Filter to validate incoming and modify 
    // outgoing data from action methods.
    /// </summary>
    public class CustomAsyncActionFilter : Attribute, IAsyncActionFilter
    {
        private readonly IMessageCodeService _messageCodeService;
        public CustomAsyncActionFilter(IMessageCodeService messageCodeService) =>
           _messageCodeService = messageCodeService;
       
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // validate input parameters before the action executes
            var actionArguments = context.ActionArguments;
            if (actionArguments.Count > 0)
            {
                if (actionArguments.ContainsKey("name"))
                {
                    if (actionArguments["name"].ToString().Length < ValidationConstants.NameSearchLengthLimit)
                    { 
                        context.Result = SearchFieldTooShort();
                        return;
                    }                     
                }
            }

            // next() calls the action method
            var resultContext = await next();
            var result = resultContext.Result;  
            
            if (result !=null)                            
                resultContext.Result = ErrorResult((IEnumerable<object>) ((ObjectResult)result).Value);            
        }

        private IActionResult SearchFieldTooShort()
        {
            return CheckForUnprocessableEntity(CodeConstants.SearchFieldTooShort);           
        }

        private IActionResult SearchResultsExceeded()
        {
            return CheckForUnprocessableEntity(CodeConstants.SearchResultsExceededLimit);           
        }

        /// <summary>
        ///Returns the output upon checking fo output limit.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        private IActionResult ErrorResult<T>(IEnumerable<T> result) where T : class =>
           result switch
           {
               null => new NotFoundResult(),
               { } _ when result.LimitExceeded() => SearchResultsExceeded(),
               _ => new OkObjectResult(result)
           };

        private IActionResult CheckForUnprocessableEntity(string value)
        {
            var messageCode = _messageCodeService.GetMessageByCode(value);

            return ApiResponse.UnprocessableEntity(
                ProcessMessage.ProcessMessageFromMessageCode(messageCode));
        }
    }
}
