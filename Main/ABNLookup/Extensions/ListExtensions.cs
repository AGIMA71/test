using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ABNLookup.Validation;

namespace ABNLookup.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> list, string sortExpression) where T:class
        {
            if (string.IsNullOrEmpty(sortExpression))
                return list;

            sortExpression += "";
            bool descending = false;
            string property = "";
            Regex trimmer = new Regex(@"\s\s+");
            string[] parts = trimmer.Replace(sortExpression, " ").Trim().Split(null);

            property = parts[0];
            if (parts.Length > 1)
            {
                descending = parts[1].ToLower() == "desc";
            }

            PropertyInfo prop = typeof(T).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (descending)
                return list.OrderByDescending(x => prop.GetValue(x, null));
            else
                return list.OrderBy(x => prop.GetValue(x, null));
        }

        public static bool LimitExceeded<T>(this IEnumerable<T> list) where T : class => list.Count() > ValidationConstants.SearchResultsLimit;

    }
}
