using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ABNLookup.Hypermedia;
using ABNLookup.Interfaces;
using ABNLookup.Constants;

namespace ABNLookup.Services
{
    public class HypermediaService :IHypermediaService
    {
        private readonly LinkGenerator linkGenerator;
        private readonly ILinkData linkData;

        public HypermediaService(LinkGenerator linkGenerator, ILinkData linkData) =>
            (this.linkGenerator, this.linkData) = (linkGenerator, linkData);

        /// <summary>
        /// Given the  action name, controller name and parameter values to action methods, this function generates a relative url.
        /// eg. /api/v1/controller/paratmerValue
        /// apiVersion is part of parameter values.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="values"></param>
        /// <param name="method"></param>
        /// <param name="rel"></param>
        /// <returns></returns>
        private Link GetResourceLink(HttpContext httpContext,string action, string controller, object values, string method, string rel ) =>
            new Link
            {
                Href = linkGenerator.GetPathByAction(httpContext, action: action, controller: controller, values: values),
                Method = method,
                Rel = rel
            };

        /// <summary>
        /// Tuple<Relation,Parameter values,controllerAction,Htttpmethod>
        /// iterates through sample data and generates links by calling GetResourceLink()
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="controller"></param>
        /// <param name="apiVersion"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public ResourceLink GetAbnResouceLinks(HttpContext httpContext, string controller, string apiVersion, string[] actions)
        {
            var data = linkData.GetAbnLinkData(apiVersion, AbnLookupConstants.SortFieldName, actions);

            return GetResouceLinks(httpContext, controller, data);
        }

        /// <summary>
        /// Tuple<Relation,Parameter values,controllerAction,Htttpmethod>
        /// iterates through sample data and generates links by calling GetResourceLink()
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="controller"></param>
        /// <param name="apiVersion"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public ResourceLink GetOrgResouceLinks(HttpContext httpContext, string controller, string apiVersion, string[] actions)
        {
            var data = linkData.GetOrgLinkData(apiVersion, AbnLookupConstants.SortFieldName, actions);

            return GetResouceLinks(httpContext, controller, data);
        }

        private ResourceLink GetResouceLinks(HttpContext httpContext, string controller, (string, object, string, string)[] data)
        {
            var resourceLink = new ResourceLink();

            resourceLink.Links.AddRange(from eachItem in data
                select GetResourceLink(httpContext,
                    eachItem.Item3,
                    controller,
                    eachItem.Item2,
                    eachItem.Item4,
                    eachItem.Item1));

            return resourceLink;
        }
    }
}
