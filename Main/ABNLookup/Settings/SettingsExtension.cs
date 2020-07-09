using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ABNLookup.Settings
{
    // Extension that validates the data annotations on an object, used for app settings.
    // REF: https://odetocode.com/blogs/scott/archive/2011/06/29/manual-validation-with-data-annotations.aspx
    public static class SettingsExtensions
    {
        public static bool IsValid<T>(this T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var validationResult = new List<ValidationResult>();
            var result = Validator.TryValidateObject(data, new ValidationContext(data), validationResult, false);

            if (!result)
            {
                foreach (var item in validationResult)
                {
                    Debug.WriteLine($"ERROR::{item.MemberNames}:{item.ErrorMessage}");
                }
            }

            return result;
        }
    }
}