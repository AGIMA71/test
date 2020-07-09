namespace ABNLookup.Interfaces
{
    public interface ILinkData
    {
        (string, object, string, string)[] GetAbnLinkData(string version, string sortField, string[] actions);
        (string, object, string, string)[] GetOrgLinkData(string version, string sortField, string[] actions);
    }
}