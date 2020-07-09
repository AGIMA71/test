using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace ABNLookup.Validation
{
    // REF:: https://api.gov.au/standards/national_api_standards/error-handling.html#error-response-payload
    public class ValidationErrors
    {
        public static ValidationErrors SingleError(ProcessMessage processMessage) =>
            new ValidationErrors
            {
                ProcessMessages = new List<ProcessMessage> { processMessage }
            };

        public static ValidationErrors MultipleErrors(IList<ProcessMessage> processMessages) =>
            new ValidationErrors
            {
                ProcessMessages = processMessages
            };

        [JsonPropertyName("errors")]
        public ICollection<ProcessMessage> ProcessMessages { get; set; }

        public ValidationErrors(ModelStateDictionary modelState)
        {
            // TODO: key in model validation needs to map to source/parameter later as per api.gov.au/standards
            ProcessMessages = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ProcessMessage(ValidationConstants.ModelValidationErrorCode, $"Field: {key} - {x.ErrorMessage}")))
                    .ToList();
        }

        public ValidationErrors() { }
        
    }
}