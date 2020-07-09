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
using ABNLookup.Dtos;
using System.Text.RegularExpressions;
using System;

namespace ABNLookup.Services
{
    public class AbnService : IAbnService
    {
        private readonly AbnContext abnContext;
        private readonly IMapper mapper;
        private readonly IHypermediaService hypermediaService;

        public AbnService(AbnContext abnContext, IMapper mapper, IHypermediaService hypermediaService) =>
            (this.abnContext, this.mapper, this.hypermediaService) = (abnContext, mapper, hypermediaService);

        public async Task<IEnumerable<T>> GetAbnAsync<T>(string id, string sortExpression = "") where T : class
        {
            string[] ids = id.Split(',');

            var matchedList = await abnContext.Abns
                .LikeAny(nameof(Abn.ABNidentifierValue), ids)
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

        public ResourceLink GetAbnResourceLinks(HttpContext httpContext, string apiVersion, string[] actions) 
        {
            string controller = apiVersion == "1" ? AbnLookupConstants.AbnController : AbnLookupConstants.AbnV2Controller;

            return hypermediaService.GetAbnResouceLinks(httpContext,
                controller,
                apiVersion,
                actions);
        }

        public async  Task<bool> BusniessNameAlreadyRegistered(string businessName)
        {
            return await abnContext.Abns.FirstOrDefaultAsync(x => x.MainNameorganisationName.ToLower().Trim() == businessName.ToLower().Trim()) !=null;           
        }

        public async Task<AbnNewDTO> Register(AbnRegisterDTO model)
        {

            var maxInternalClientId = await abnContext.Abns.Select(i => i.ClientInternalId).MaxAsync();
            var maxAbn = abnContext.Abns.Select(x => long.Parse(x.ABNidentifierValue)).ToList().Max();

            var abn = mapper.Map<Abn>(model);
            abn.ClientInternalId = maxInternalClientId + 1;
            var newAbnValue = (maxAbn + 1).ToString();
            var newAcnValue = newAbnValue.Substring(2, newAbnValue.Length-2);
            abn.ABNidentifierValue = newAbnValue;
            abn.ACNidentifierValue = newAcnValue;           

            var savedEntity = abnContext.Abns.Add(abn);
            await abnContext.SaveChangesAsync();

            var abnDtoToReturn = mapper.Map<AbnNewDTO>(savedEntity.Entity);
            return abnDtoToReturn;
        }
    }
}