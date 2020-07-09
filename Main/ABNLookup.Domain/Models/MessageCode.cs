namespace ABNLookup.Domain.Models
{
    public class MessageCode
    {
        public string Code { get; }
        public string Description { get; }

        public MessageCode()
        { }

        public MessageCode(string code, string description) : this() =>
            (Code, Description) = (code, description);
    }
}