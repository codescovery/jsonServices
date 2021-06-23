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
            fromObject.Merge(toObject, new JsonMergeSettings
            {
                MergeNullValueHandling = MergeNullValueHandling.Merge,
                MergeArrayHandling = MergeArrayHandling.Merge
            });
            return fromObject.ToString(writeIdented ? Formatting.Indented : Formatting.None);
        }
    }
}