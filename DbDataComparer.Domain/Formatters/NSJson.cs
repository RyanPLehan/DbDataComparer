using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace DbDataComparer.Domain.Formatters
{
    /// <summary>
    /// Newtonsoft Json Implementation
    /// </summary>
    public static class NSJson
    {
        public const string JSON_CONTENT_TYPE = @"application/json";

        public static JsonSerializerSettings DefaultJsonSerializerOptions = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
        };

        public static T Deserialize<T>(string json)
        {
            return Deserialize<T>(json, null);
        }

        public static T Deserialize<T>(string json, JsonSerializerSettings jsonSerializerOptions)
        {
            var jsonOptions = jsonSerializerOptions ?? DefaultJsonSerializerOptions;
            return String.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json, jsonOptions);
        }

        public static IEnumerable<T> Deserialize<T>(IEnumerable<string> json)
        {
            return Deserialize<T>(json, null);
        }

        public static IEnumerable<T> Deserialize<T>(IEnumerable<string> json, JsonSerializerSettings jsonSerializerOptions)
        {
            var jsonOptions = jsonSerializerOptions ?? DefaultJsonSerializerOptions;
            return json == null ? Enumerable.Empty<T>() : json.Where(x => !String.IsNullOrWhiteSpace(x))
                                                              .Select(x => JsonConvert.DeserializeObject<T>(x, jsonOptions))
                                                              .ToArray();
        }

        public static string Serialize(object value)
        {
            return Serialize(value, null);
        }

        public static string Serialize(object value, JsonSerializerSettings jsonSerializerOptions)
        {
            var jsonOptions = jsonSerializerOptions ?? DefaultJsonSerializerOptions;
            return value == null ? null : JsonConvert.SerializeObject(value, jsonOptions);
        }
    }
}
