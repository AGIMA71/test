namespace ABNLookup.Constants
{

    public static class AbnLookupConstants
    {
        public static readonly string Self = "_self";
        public static readonly string CommaSeparated = "comma_separated";
        public static readonly string SortDefaultAsc = "sort_default_asc";
        public static readonly string SortAscending = "sort_asc";
        public static readonly string SortDescending = "sort_desc";

        public static readonly string AbnSingleValue = "12089523919";
        public static readonly string AbnCommaSepartedValues = "12089523919,83080438419";

        public static readonly string Ascending = "asc";
        public static readonly string Descending = "desc";

        public static readonly string OrgNameSingleValue = "AAC";
        public static readonly string OrgNameCommaSepartedValues = "AAC,ABRA";

        public static readonly string ApiVersion1= "1";
        public static readonly string ApiVersion2 = "2";
       
        public static readonly string SortFieldName = "sortFieldName";

        public static readonly string[] AbnV1Actions = { "GetAbnV1","CreateAbnV1","UpdateAbnV1", "DeleteAbnV1" };
        public static readonly string[] AbnV2Actions = { "GetAbnV2", "CreateAbnV2", "UpdateAbnV2", "DeleteAbnV2" };

        public static readonly string[] AcnV1Actions = { "GetAcnV1", "CreateAcnV1", "UpdateAcnV1", "DeleteAcnV1" };
        public static readonly string[] AcnV2Actions = { "GetAcnV2", "CreateAcnV2", "UpdateAcnV2", "DeleteAcnV2" };

        public static readonly string[] OrgV1Actions = { "GetOrgName", "CreateOrgV1", "UpdateOrgV1", "DeleteOrgV1" };
        public static readonly string[] OrgV2Actions = { "GetOrgNameV2", "CreateOrgV2", "UpdateOrgV2", "DeleteOrgV2" };

        public static readonly string HttpGet= "GET";
        public static readonly string HttpPost = "POST"; 
        public static readonly string HttpPut = "PUT";
        public static readonly string HttpDelete = "DELETE";

        public static readonly string AbnController = "abn";
        public static readonly string AcnController = "acn";
        public static readonly string OrgNameController = "orgname";
        public static readonly string AbnV2Controller = "abnV2";
        public static readonly string AcnV2Controller = "acnV2";
        public static readonly string OrgNameV2Controller = "orgnameV2";

        public static readonly string DeprecatedHeader = "X-API-Deprecated";
        public static readonly string RetirementHeader = "X-API-Retire-Time";

        public static readonly string AppSettings = "AppSettings";

        public const string Max50CharactersAllowed = "Max 50 characters allowed";
        public const string Minium3CharactersLong = "Business Name should be mimimum 3 characters long";
        public const string InvalidCharactersInBusinessName = "Business name contains invalid characters. Allowed character are alphanumeric and optional special character _@.#&+-()'";
        public const string OptionalSpecialCharactersMessage = "Business name contains invalid characters. Optional one per each special character _@.#&+-()'";
    }
}
