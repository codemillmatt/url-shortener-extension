using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json
                ;
namespace ShortUrl.Core
{
    public partial class ShortResponse
    {
        [JsonProperty("ShortUrl")]
        public string ShortUrl { get; set; }

        [JsonProperty("LongUrl")]
        public string LongUrl { get; set; }
    }

    public partial class ShortResponse
    {
        public static List<ShortResponse> FromJson(string json) => JsonConvert.DeserializeObject<List<ShortResponse>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<ShortResponse> self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this ShortRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
