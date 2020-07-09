using System;
using System.Collections.Generic;
using System.Text;
using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace ABNLookup.Domain.Models
{
    public  class ApiInfo
    {
        [J("api_version")]
        public string  Version { get; set; }
        [J("api_status")]
        public string Status { get; set; }
    }
}
