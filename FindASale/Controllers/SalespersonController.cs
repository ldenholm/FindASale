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
    public class SalespersonController : ControllerBase
    {
        private readonly IAgent _agent;
        public SalespersonController(IAgent agent)
        {
            _agent = agent;
        }

        //[HttpPost]
        //public Result Assign(CustomerFormDTO form)
        //{
        //    return _agent.ProcessAssignment(form);
        //}

        [HttpPost]
        public CustomerFormDTO Assign(CustomerFormDTO form)
        {
            return form;
        }
    }
}
