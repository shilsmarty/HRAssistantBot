using System;
using System.Collections.Generic;
using System.Text;

namespace MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Models
{
    public class HealthInsurance
    {
        public decimal? CoinsuranceMax { get; set; }
        public decimal? CoinsuranceUsed { get; set; }
        public decimal? DeductableMax { get; set; }
        public decimal? DeductibleUsed { get; set; }
        public decimal? OutOfPocketLeft { get; set; }
        public decimal? OutOfPocketMax { get; set; }
        public string PlanDescription { get; set; }
    }
}
