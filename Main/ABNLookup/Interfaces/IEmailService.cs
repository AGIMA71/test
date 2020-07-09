using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABNLookup.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(Exception exception);
    }
}
