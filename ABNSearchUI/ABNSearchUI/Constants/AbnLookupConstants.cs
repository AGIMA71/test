using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABNSearchUI.Constants
{
    public class AbnLookupConstants
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

        public static readonly string ApiVersion1 = "1";
        public static readonly string ApiVersion2 = "2";

        public static readonly string SortFieldName = "sortFieldName";

        public static readonly string[] AbnV1Actions = { "GetAbnV1", "CreateAbnV1", "UpdateAbnV1", "DeleteAbnV1" };
        public static readonly string[] AbnV2Actions = { "GetAbnV2", "CreateAbnV2", "UpdateAbnV2", "DeleteAbnV2" };

        public static readonly string[] AcnV1Actions = { "GetAcnV1", "CreateAcnV1", "UpdateAcnV1", "DeleteAcnV1" };
        public static readonly string[] AcnV2Actions = { "GetAcnV2", "CreateAcnV2", "UpdateAcnV2", "DeleteAcnV2" };

        public static readonly string[] OrgV1Actions = { "GetOrgName", "CreateOrgV1", "UpdateOrgV1", "DeleteOrgV1" };
        public static readonly string[] OrgV2Actions = { "GetOrgNameV2", "CreateOrgV2", "UpdateOrgV2", "DeleteOrgV2" };

        public static readonly string HttpGet = "GET";
        public static readonly string HttpPost = "POST";
        public static readonly string HttpPut = "PUT";
        public static readonly string HttpDelete = "DELETE";

        public static readonly string AbnController = "abn";
        public static readonly string AcnController = "acn";
        public static readonly string OrgNameController = "orgname";
    }
}
