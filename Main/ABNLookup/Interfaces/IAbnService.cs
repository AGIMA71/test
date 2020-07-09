using ABNLookup.Dtos;
using ABNLookup.Hypermedia;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABNLookup.Interfaces
{
    public interface IAbnService
    {
        Task<IEnumerable<T>> GetAbnAsync<T>(string id, string sortExpression = "") where T : class;        
        ResourceLink GetAbnResourceLinks(HttpContext httpContext, string apiVersion,string[] actions);
        Task<AbnNewDTO> Register(AbnRegisterDTO model);
        Task<bool> BusniessNameAlreadyRegistered(string businessName);
    }
}