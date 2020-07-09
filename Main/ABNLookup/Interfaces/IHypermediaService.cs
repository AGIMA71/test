using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Http;

namespace ABNLookup.Interfaces
{
    public interface IHypermediaService
    {
        ResourceLink GetAbnResouceLinks(HttpContext httpContext, string controller, string apiVersion, string[] actions);
        ResourceLink GetOrgResouceLinks(HttpContext httpContext, string controller, string apiVersion, string[] actions);
    }
}