using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace ABNLookup.Data
{
    public struct ProcessMessagesFileModel
    {
        [J("code")]
        public string Code { get; set; }

        [J("description")]
        public string Description { get; set; }
    }
}
