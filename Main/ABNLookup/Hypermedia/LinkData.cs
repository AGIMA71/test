using ABNLookup.Constants;
using ABNLookup.Interfaces;

namespace ABNLookup.Hypermedia
{
    /// <summary>
    /// Prepare sample data that goes into Resource Urls.
    /// </summary>
    public class LinkData : ILinkData
    {
        // ValueTuple<Relation,Parameter values,controllerAction,Htttpmethod>
        public (string, object, string, string)[] GetAbnLinkData(string version, string sortField, string[] actions)
        {
            object values1 = new { version = version, id = AbnLookupConstants.AbnSingleValue };
            object values2 = new { version = version, id = AbnLookupConstants.AbnCommaSepartedValues };
            object values3 = new { version = version, id = AbnLookupConstants.AbnCommaSepartedValues, sort = sortField };
            object values4 = new { version = version, id = AbnLookupConstants.AbnCommaSepartedValues, sort = sortField + " " + AbnLookupConstants.Ascending };
            object values5 = new { version = version, id = AbnLookupConstants.AbnCommaSepartedValues, sort = sortField + " " + AbnLookupConstants.Descending };
            string getAction = actions[0];

            // add Create, update and delete actions to the tuple in future eg. actions[1]=Post,actions[2]=update etc.
            return new (string, object, string, string)[] {
                (AbnLookupConstants.Self, values1, getAction, AbnLookupConstants.HttpGet),
                (AbnLookupConstants.CommaSeparated, values2, getAction, AbnLookupConstants.HttpGet),
                (AbnLookupConstants.SortDefaultAsc, values3, getAction, AbnLookupConstants.HttpGet),
                (AbnLookupConstants.SortAscending, values4, getAction, AbnLookupConstants.HttpGet),
                (AbnLookupConstants.SortDescending, values5, getAction, AbnLookupConstants.HttpGet)
            };
        }

        // ValueTuple<Relation,Parameter values,controllerAction,Htttpmethod>
        public (string, object, string, string)[] GetOrgLinkData(string version, string sortField, string[] actions)
        {
            object values1 = new { version = version, name = AbnLookupConstants.OrgNameSingleValue };
            object values2 = new { version = version, name = AbnLookupConstants.OrgNameCommaSepartedValues };
            object values3 = new { version = version, name = AbnLookupConstants.OrgNameCommaSepartedValues, sort = sortField };
            object values4 = new { version = version, name = AbnLookupConstants.OrgNameCommaSepartedValues, sort = sortField + " " + AbnLookupConstants.Ascending };
            object values5 = new { version = version, name = AbnLookupConstants.OrgNameCommaSepartedValues, sort = sortField + " " + AbnLookupConstants.Descending };
            string getAction = actions[0];

            return new (string, object, string, string)[] {
                (AbnLookupConstants.Self, values1, getAction, AbnLookupConstants.HttpGet),
                (AbnLookupConstants.CommaSeparated, values2, getAction, AbnLookupConstants.HttpGet),
                (AbnLookupConstants.SortDefaultAsc, values3, getAction, AbnLookupConstants.HttpGet),
                (AbnLookupConstants.SortAscending, values4, getAction, AbnLookupConstants.HttpGet),
                (AbnLookupConstants.SortDescending, values5, getAction, AbnLookupConstants.HttpGet)
            };
        }
    }
}