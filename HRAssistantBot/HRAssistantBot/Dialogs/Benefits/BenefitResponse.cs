using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FHL.Hack.SmartAssistantBot.Dialogs.Benefits
{
    public class BenefitResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("benefits")]
        public List<Benefit> Benefits { get; set; }
    }
}
