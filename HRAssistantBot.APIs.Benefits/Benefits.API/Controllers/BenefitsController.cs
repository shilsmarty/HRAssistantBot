using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Models;
using MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Services;

namespace MS.CFE.FHL.SmartAssistant.APIs.Benefits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitsController : ControllerBase
    {
        private IBenefitsManager _benefitsManager;
        public BenefitsController(IBenefitsManager benefitsManager)
        {
            _benefitsManager = benefitsManager;
        }
        // GET: api/Benefits/5
        [HttpGet("{id}", Name = "Get")]
        public BenefitsResponse Get(int id)
        {
            return _benefitsManager.GetBenefitsAsync();
        }

       
    }
}
