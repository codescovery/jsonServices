using Codescovery.JsonServices.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Codescovery.JsonServices.Services
{
    internal class JsonService:IJsonService
    {
        public IJsonPath ConvertToJsonPath(string json)
        {
            var jsonPath = new JsonPath(json)
                            .CreateJsonPath();
            return jsonPath;
        }

        public string OverrideJsonValuesFromPath(string jsonFrom, string jsonTo,bool writeIdented = true)
        {
            var overridedJson = JObject.Parse(jsonFrom);
            var environmentJsonObject = JObject.Parse(jsonTo);
            var jsonPath = new JsonPath(jsonTo).CreateJsonPath().CreateJsonPath();
            foreach (var environmentPath in jsonPath.GetJsonPaths())
            {
                var token = overridedJson.SelectToken(environmentPath.Key);
                if (token != null) continue;
                overridedJson.Add(environmentPath.Key, environmentJsonObject.SelectToken(environmentPath.Key));
            }

            return overridedJson.ToString(writeIdented ? Formatting.Indented : Formatting.None);
        }
    }
}