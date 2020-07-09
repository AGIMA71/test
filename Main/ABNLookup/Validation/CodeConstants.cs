using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABNLookup.Validation
{
    public static class CodeConstants
    {
        public static readonly string MissingMandatoryField = "1001";
        public static readonly string SearchFieldTooShort = "1002";
        public static readonly string SearchResultsExceededLimit = "1003";
        public static readonly string InValidSortField = "1004";
        public static readonly string SortFieldNameIncorrect = "1005";
        public static readonly string BusinssNmeAlreadyExists = "1006";
    }
}
