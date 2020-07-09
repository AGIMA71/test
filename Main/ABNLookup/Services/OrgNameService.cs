using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ABNLookup.Extensions;
using ABNLookup.Interfaces;
using ABNLookup.Hypermedia;
using ABNLookup.Constants;
using ABNLookup.Domain.Models;
using ABNLookup.Infrastructure;
using ABNLookup.Infrastructure.Extensions;

namespace ABNLookup.Services
{
    public class OrgNameService : IOrgNameService
    {
        private readonly AbnContext abnContext;
        private readonly IMapper mapper;
        private readonly IHypermediaService hypermediaService;

        public OrgNameService(AbnContext abnContext, IMapper mapper, IHypermediaService hypermediaService) =>
            (this.abnContext, this.mapper, this.hypermediaService) = (abnContext, mapper, hypermediaService);

        public async Task<IEnumerable<T>> GetOrgAsync<T>(string id, string sortExpression = "") where T : class
        {
            string[] ids = (id.ToUpper().Split(',')).Select(t => t.Trim()).ToArray();

            var matchedList = await abnContext.Abns
                .LikeAny(nameof(Abn.MainNameorganisationName), ids)
                .AsNoTracking()
                .ToListAsync();

            if (matchedList?.Count() > 0)
            {
                var mappedList = mapper.Map<IEnumerable<T>>(matchedList);
                var sortedList = mappedList.Sort(sortExpression);
                return sortedList;
            }
            return null;
        }

        public ResourceLink GetOrgResourceLinks(HttpContext httpContext, string apiVersion, string[] actions)
        {
            string controller = apiVersion == "1" ? AbnLookupConstants.OrgNameController : AbnLookupConstants.OrgNameV2Controller;
            return hypermediaService.GetOrgResouceLinks(httpContext,
                controller,
                apiVersion,
                actions);
        }
            
    }
}