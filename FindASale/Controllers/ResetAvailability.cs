using FindASale.Models;
using FindASale.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindASale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetAvailability : ControllerBase
    {
        private readonly IAgent _agent;
        public ResetAvailability(IAgent agent)
        {
            _agent = agent;
        }

        [HttpGet]
        public ResetResult TriggerResetAvailability()
        {
            return _agent.ResetAvailability();
        }
    }
}
