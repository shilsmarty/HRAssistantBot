using Newtonsoft.Json;
using System.Collections.Generic;

namespace FHL.Hack.SmartAssistantBot.Dialogs.Benefits
{
    public class Benefit
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("tagline")]
        public string Tagline { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("relevance")]
        public decimal Relevance { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        [JsonProperty("hiddenessQuotient")]
        public int HiddenessQuotient { get; set; }
    }
}
