using System.Collections.Generic;

namespace Codescovery.JsonServices.Interfaces
{
    public interface IJsonPath
    {
        IReadOnlyDictionary<string, string> GetJsonPaths();
        string GetPathValue(string path);
        IJsonPath CreateJsonPath();
    }
}