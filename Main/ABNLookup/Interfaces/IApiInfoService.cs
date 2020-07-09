using ABNLookup.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABNLookup.Interfaces
{
    public interface IApiInfoService
    {
        ApiInfo GetApiInfo();
        ApiInfo GetApiV2Info();
    }
}
