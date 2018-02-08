using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShortUrl.Core
{
    public partial class ShortRequest
    {
        [JsonProperty("TagUtm")]
        public bool TagUtm { get; set; }

        [JsonProperty("TagWt")]
        public bool TagWt { get; set; }

        [JsonProperty("Campaign")]
        public string Campaign { get; set; }

        [JsonProperty("Mediums")]
        public List<string> Mediums { get; set; }

        [JsonProperty("Input")]
        public string Input { get; set; }
    }

    public partial class ShortRequest
    {
        public static ShortRequest FromJson(string json) => JsonConvert.DeserializeObject<ShortRequest>(json, Converter.Settings);
    }
}
