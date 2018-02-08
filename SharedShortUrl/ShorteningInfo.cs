using System;

using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace SharedShortUrl
{
    public class ShorteningInfo
    {
        public ShorteningInfo()
        {
        }

        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("tagSource")]
        public bool TagSource { get; set; }

        [JsonProperty("tagMediums")]
        public bool TagMediums { get; set; }

        [JsonProperty("mediumName")]
        public string MediumName { get; set; }

        [JsonProperty("campaignName")]
        public string CampaignName { get; set; }

        [JsonProperty("appendMedium")]
        public bool AppendMedium { get; set; }
    }

    public class ShortenedUrls
    {
        [JsonProperty("ShortUrl")]
        public string ShortUrl { get; set; }

        [JsonProperty("LongUrl")]
        public string LongUrl { get; set; }
    }
}
