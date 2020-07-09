using ABNLookup.Interfaces;
using ABNLookup.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ABNLookup.Services
{
    /// <summary>
    /// Initally just used to validate sort properties as part of some refactoring.
    /// Service responsibilty to be extended later to handle mapping between DTO search fields and domain search fields.
    /// For eg. DTO object properties that are being searched on could map to multiple domain object properties.
    ///     -> For example, we may expose just a "Name" field on the DTO but we need the search expression query on the domain object to be mapped to "Surname, GivenName asc".
    /// </summary>
    public class SortMappingService : ISortMappingService
    {
        private readonly IMessageCodeService _messageCodeService;

        public SortMappingService(IMessageCodeService messageCodeService)
        {
            _messageCodeService = messageCodeService;
        }

        public IEnumerable<ProcessMessage> ValidateSortExpression(Type t, string sortExpression)
        {
            var result = new List<ProcessMessage>();
            if (string.IsNullOrEmpty(sortExpression))
            {
                return result;
            }

            string[] sortExpressionParts = new Regex(@"\s\s+").Replace(sortExpression, " ").Trim().Split(null);
            if ((sortExpressionParts.Length > 1) &&
                (sortExpressionParts[1].ToLower() != "asc" && sortExpressionParts[1].ToLower() != "desc"))
            {
                result.Add(ProcessMessage.ProcessMessageFromMessageCode(_messageCodeService.GetMessageByCode(CodeConstants.InValidSortField)));
            }

            // Currently only supporting a single field sort do don't need to split on comma separated values.
            var sortField = sortExpressionParts[0];

            var prop = t.GetProperties().FirstOrDefault(p => string.Equals(p.Name, sortField, StringComparison.OrdinalIgnoreCase));
            if (prop == null)
            {
                result.Add(ProcessMessage.ProcessMessageFromMessageCode(_messageCodeService.GetMessageByCode(CodeConstants.SortFieldNameIncorrect)));
            }

            return result;
        }

    }
}
