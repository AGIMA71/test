using System;
using System.Text.Json.Serialization;
using ABNLookup.Domain.Models;

namespace ABNLookup.Validation
{
    // REF: https://api.gov.au/standards/national_api_standards/error-handling.html#error-response-payload
    public class ProcessMessage
    {
        public ProcessMessage() => Id = Guid.NewGuid().ToString();
        public ProcessMessage(string code, string description) : this() =>
            (Code, Description) = (code, description);

        // mandatory
        [JsonPropertyName("code")]
        public string Code { get; set; }

        // mandatory
        [JsonPropertyName("detail")]
        public string Description { get; set; }

        // optional
        [JsonPropertyName("id")]
        public string Id { get; private set; }

        public static ProcessMessage ProcessMessageFromMessageCode(MessageCode messageCode) =>
            new ProcessMessage
            {
                Code = messageCode.Code,
                Description = messageCode.Description,
                Id = Guid.NewGuid().ToString()
            };
    }
}