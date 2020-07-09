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
    public class AcnService : IAcnService
    {
        private readonly AbnContext abnContext;
        private readonly IMapper mapper;
        private readonly IHypermediaService hypermediaService;

        public AcnService(AbnContext abnContext, IMapper mapper, IHypermediaService hypermediaService) =>
            (this.abnContext, this.mapper, this.hypermediaService) = (abnContext, mapper, hypermediaService);

        public async Task<IEnumerable<T>> GetAcnAsync<T>(string id, string sortExpression = "") where T : class
        {
            string[] ids = id.Split(',');

            var matchedList = await abnContext.Abns
                .LikeAny(nameof(Abn.ACNidentifierValue), ids)
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
        public ResourceLink GetAcnResourceLinks(HttpContext httpContext, string apiVersion, string[] actions)
        {
            string controller = apiVersion == "1" ? AbnLookupConstants.AcnController : AbnLookupConstants.AcnV2Controller;
            return hypermediaService.GetAbnResouceLinks(httpContext,
                controller,
                apiVersion,
                actions);
        }
           
    }
}