using System.Linq;
using ABNLookup.Interfaces;
using ABNLookup.Domain.Models;
using ABNLookup.Infrastructure;

namespace ABNLookup.Services
{
    public class MessageCodeService : IMessageCodeService
    {
        private readonly AbnContext abnContext;

        public MessageCodeService(AbnContext abnContext) =>
            this.abnContext = abnContext;

        public MessageCode GetMessageByCode(string code) =>
            abnContext.MessageCodes.Single(x => x.Code == code);
    }
}