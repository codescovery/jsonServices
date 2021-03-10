namespace Codescovery.JsonServices.Interfaces
{
    public interface IJsonService
    {
        IJsonPath ConvertToJsonPath(string json);
        string OverrideJsonValuesFromPath(string jsonFrom, string jsonTo, bool writeIdented = true);
    }
}