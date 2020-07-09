using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABNLookup.Interfaces
{
    public interface IAcnService
    {
        Task<IEnumerable<T>> GetAcnAsync<T>(string id, string sortExpression = "") where T : class;
        ResourceLink GetAcnResourceLinks(HttpContext httpContext, string apiVersion, string[] actions);
    }
}