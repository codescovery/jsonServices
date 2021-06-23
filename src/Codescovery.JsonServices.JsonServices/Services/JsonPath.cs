using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Codescovery.JsonServices.Interfaces;

namespace Codescovery.JsonServices.Services
{
    internal class JsonPath:IJsonPath
    {
        private readonly string _json;
        private readonly Dictionary<string, string> _jsonPaths;

        public JsonPath(string json)
        {
            _json = json;
            _jsonPaths = new Dictionary<string, string>();
        }

        private IReadOnlyDictionary<string, string> ConvertJsonPathsKeysToLowerInvariant()
        {
            return _jsonPaths.Keys.ToDictionary(key => key.ToLower(), key => _jsonPaths[key]);
        }

        public IReadOnlyDictionary<string, string> GetJsonPaths() => _jsonPaths;

        public string GetPathValue(string path)
        {
            var jsonPaths = ConvertJsonPathsKeysToLowerInvariant();
           return !jsonPaths.ContainsKey(path.ToLowerInvariant()) ? null : jsonPaths[path.ToLowerInvariant()];
        }

        public IJsonPath CreateJsonPath()
        {
            _jsonPaths.Clear();
            using var document = JsonDocument.Parse(_json);
            foreach (var jsonProperty in document.RootElement.EnumerateObject())
                ParseJsonProperty(jsonProperty);

            try
            {
                document?.Dispose();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while disponsing JsonDocument object {ex.Message}");
            }

            return this;
        }
        private void ParseJsonObject(JsonElement jsonElement,  string path)
        {
            _jsonPaths.Add(path, jsonElement.ToString());
            foreach (var jsonProperty in jsonElement.EnumerateObject())
                ParseJsonProperty(jsonProperty, path);
        }
        private void ParseJsonArray(JsonElement jsonElement,  string path)
        {
            var arrayIndex = 0;
            _jsonPaths.Add(path, jsonElement.ToString());
            foreach (var jsonArrayElement in jsonElement.EnumerateArray())
            {
                var jsonArrayPath = $"{path}[{arrayIndex}]";
                switch (jsonArrayElement.ValueKind)
                {
                    case JsonValueKind.Object:
                        foreach (var jsonProperty in jsonArrayElement.EnumerateObject())
                            ParseJsonProperty(jsonProperty,  jsonArrayPath);
                        break;
                    default:
                        ParseJsonElement(jsonArrayElement,  jsonArrayPath);
                        break;
                }
                arrayIndex++;
            }
        }

        private void ParseJsonElement(JsonElement jsonElement,  string path)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Object:
                    ParseJsonObject(jsonElement,  path);
                    break;
                case JsonValueKind.Array:
                    ParseJsonArray(jsonElement,  path);
                    break;
                default:
                    _jsonPaths.Add(path, jsonElement.ToString());
                    break;
            }
        }
        private void ParseString(JsonProperty jsonProperty,  string path)
        => ParseJsonElement(jsonProperty.Value,  path);
        
        private void ParseJsonProperty(JsonProperty jsonProperty, string path = null)
        {
            path = (path == null) ? jsonProperty.Name : $"{path}.{jsonProperty.Name}";
            switch (jsonProperty.Value.ValueKind)
            {
                case JsonValueKind.String:
                    ParseString(jsonProperty,  path);
                    break;
                case JsonValueKind.Object:
                    ParseJsonObject(jsonProperty.Value,  path);
                    break;
                case JsonValueKind.Array:
                    ParseJsonArray(jsonProperty.Value,  path);
                    break;
                default:
                    ParseString(jsonProperty,  path);
                    break;
            }

        }
    }
}
