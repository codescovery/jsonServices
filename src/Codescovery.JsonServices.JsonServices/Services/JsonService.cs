using System.Text.Json;
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
            var fromObject = JObject.Parse(jsonFrom);
            var toObject = JObject.Parse(jsonTo);
            var toObjectJsonPath = new JsonPath(jsonTo).CreateJsonPath();
            foreach (var jsonPath in toObjectJsonPath.GetJsonPaths())
            {
                var token = fromObject.SelectToken(jsonPath.Key);
                if (token != null) continue;
                fromObject.Add(jsonPath.Key, toObject.SelectToken(jsonPath.Key));
            }


            return fromObject.ToString(writeIdented ? Formatting.Indented : Formatting.None);
        }
    }
}