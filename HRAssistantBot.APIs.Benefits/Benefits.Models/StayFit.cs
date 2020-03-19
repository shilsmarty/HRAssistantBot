using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Models
{
    public class StayFit
    {

        /*<StayFitPlan><Balance>800</Balance><Credit>0</Credit><GymName>N/A</GymName><Reimbursement>0</Reimbursement><TotalAmount>800</TotalAmount></StayFitPlan>*/
        public StayFit() { }
        [JsonProperty(PropertyName = "balance", Order = 100)]
        public decimal? Balance { get; set; }

        [JsonProperty(PropertyName = "credit", Order = 110)]
        public decimal? Credit { get; set; }

        [JsonProperty(PropertyName = "reimbursement", Order = 120)]
        public decimal? Reimbursement { get; set; }

        [JsonProperty(PropertyName = "totalAmount", Order = 130)]
        public decimal? TotalAmount { get; set; }

    }
}
