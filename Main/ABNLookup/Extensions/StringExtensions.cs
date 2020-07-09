using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABNLookup.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes additional spaces, tabs with single character.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveAdditonalSpaces(this string input)
        {
            string line = input.Replace("\t", " ");
            while (line.IndexOf("  ", StringComparison.Ordinal) >= 0)
            {
                line = line.Replace("  ", " ");
            }

            return line.Trim();
        }
    }
}
