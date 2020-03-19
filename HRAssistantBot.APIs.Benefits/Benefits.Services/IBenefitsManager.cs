
using MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Services
{
    public interface IBenefitsManager
    {
        BenefitsResponse GetBenefitsAsync();
    }
}
