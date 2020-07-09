namespace ABNLookup.Domain.Models
{
    public class Abn
    {
        public long ClientInternalId { get; set; }
        public string ABNidentifierValue { get; set; }
        public string ACNidentifierValue { get; set; }
        public string MainNameorganisationName { get; set; }

        public Abn()
        { }

        public Abn(long clientInternalId,
            string abnIdentifierValue,
            string acnIdentifierValue,
            string mainNameorganisationName) : this() =>
            (ClientInternalId, ABNidentifierValue, ACNidentifierValue, MainNameorganisationName) =
            (clientInternalId, abnIdentifierValue, acnIdentifierValue, mainNameorganisationName);
    }
}