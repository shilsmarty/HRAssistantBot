using System;
using System.Collections.Generic;
using System.Text;

namespace MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Models
{
//    <ESPP>
//<ESPPContributionPercent>0.15</ESPPContributionPercent>
//<NextESPPEnrollmentDate>2018-3-01T00:00:00</NextESPPEnrollmentDate>
//</ESPP>
    public class ESPP
    {
        public ESPP() { }
        public double? ESPPContibutionPercent { get; set; }
        public DateTime? NextESPPEnrollmentDate { get; set; }
    }
}
