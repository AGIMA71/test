using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;
namespace ABNLookup.Dtos
{
    public class AcnV2DTO
    {
        [J("australianCompanyNumber")]
        public string AustralianCompanyNumber { get; set; }

        [J("mainOrganisationName")]
        public string MainOrganisationName { get; set; }
    }
}
