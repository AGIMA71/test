using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ABNLookup.Extensions;
using ABNLookup.Interfaces;
using ABNLookup.Validation;
using System.Text.RegularExpressions;
using System.Linq;

namespace ABNLookup.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMessageCodeService _messageCodeService;
        protected readonly ISortMappingService _sortMappingService;

        public BaseController(IMessageCodeService messageCodeService, ISortMappingService sortMappingService) =>
            (_messageCodeService, _sortMappingService) = (messageCodeService, sortMappingService);
            
        protected IActionResult SearchResultsExceeded()
        {
            return UnprocessableEntityWithCode(CodeConstants.SearchResultsExceededLimit);
        }

        protected IActionResult SearchFieldTooShort()
        {
            return UnprocessableEntityWithCode(CodeConstants.SearchFieldTooShort);
        }

        protected IActionResult CannotRegisterBusiness()
        {
            return UnprocessableEntityWithCode(CodeConstants.BusinssNmeAlreadyExists);
        }

        protected List<ProcessMessage> ValidateSortExpression<T>(string sortExpression) where T: class
        {
            return _sortMappingService.ValidateSortExpression(typeof(T), sortExpression).ToList();
        }

        protected IActionResult ValidationErrorsResult(IEnumerable<ProcessMessage> messages)
        {
            return ApiResponse.UnprocessableEntity(messages.ToList());
        }

        protected IActionResult ErrorResult<T>(IEnumerable<T> result) where T : class =>
            result switch
            {
                null => NotFound(),
                { } _ when result.LimitExceeded() => SearchResultsExceeded(),
                _ => null
            };

        private IActionResult UnprocessableEntityWithCode(string messageCode)
        {
            return ApiResponse.UnprocessableEntity(
                ProcessMessage.ProcessMessageFromMessageCode(_messageCodeService.GetMessageByCode(messageCode)));
        }
    }
}