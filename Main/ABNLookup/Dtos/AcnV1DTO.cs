using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace ABNLookup.Dtos
{
    public class AcnV1DTO
    {
        [J("acnIdentifierValue")]
        public string ACNidentifierValue { get; set; }

        [J("mainNameorganisationName")]
        public string mainNameorganisationName { get; set; }
    }
}
