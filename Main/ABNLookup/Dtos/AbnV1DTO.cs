using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace ABNLookup.Dtos
{
    public class AbnV1DTO
    {
      
        [J("abNidentifierValue")]
        public string ABNidentifierValue { get; set; }

        [J("mainNameorganisationName")]
        public string mainNameorganisationName { get; set; }
    }
}