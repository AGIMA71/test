using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace ABNLookup.Dtos
{
    public class AbnV2DTO
    {
        [J("australianBusinessNumber")]
        public string AustralianBusinessNumber { get; set; }

        [J("mainOrganisationName")]
        public string MainOrganisationName { get; set; }
    }
}