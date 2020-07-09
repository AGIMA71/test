using System.Collections.Generic;
using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace ABNLookup.Hypermedia
{
    public class ResourceLink
    {
        public ResourceLink()
        {

        }
        [J("_links")]
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
