using System.ComponentModel.DataAnnotations;
using ABNLookup.Attributes;
using ABNLookup.Constants;

namespace ABNLookup.Dtos
{
    public class AbnRegisterDTO
    {
        /// <summary>
        /// Does not allow all digits.
        /// Allows  optional one special character
        /// Allows alpha numeric and one special character. 
        /// </summary>
        [Required]
        [MaxLength(50,ErrorMessage = AbnLookupConstants.Max50CharactersAllowed)]
        [MinLength(3,ErrorMessage = AbnLookupConstants.Minium3CharactersLong)]
        [RegularExpression(@"^(?!\d+$)([ A-Za-z0-9]*)?([ A-Za-z0-9 _@.#&+-/(/)']+)$", ErrorMessage = AbnLookupConstants.InvalidCharactersInBusinessName)]
        [CharacterOccurrenceLimit(CharacterList = "_@.#&+-()", OccurrenceLimit = 1, ErrorMessage = AbnLookupConstants.OptionalSpecialCharactersMessage)]
        public string BusinessName { get; set; }
    }
}
