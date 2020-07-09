using ABNLookup.Domain.Models;

namespace ABNLookup.Interfaces
{
    public interface IMessageCodeService
    {
        MessageCode GetMessageByCode(string code);
    }
}