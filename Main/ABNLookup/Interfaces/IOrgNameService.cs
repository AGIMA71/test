using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABNLookup.Interfaces
{
    public interface IOrgNameService
    {
        Task<IEnumerable<T>> GetOrgAsync<T>(string id, string sortExpression = "") where T : class;      
        ResourceLink GetOrgResourceLinks(HttpContext httpContext, string apiVersion,string[] actions);
    }
}