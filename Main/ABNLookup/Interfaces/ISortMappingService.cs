using ABNLookup.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABNLookup.Interfaces
{
    public interface ISortMappingService
    {
        public IEnumerable<ProcessMessage> ValidateSortExpression(Type t, string sortExpression);
    }
}