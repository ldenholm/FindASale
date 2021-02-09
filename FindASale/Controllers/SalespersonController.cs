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
        private readonly ISalespersonRepository _salesRepo;
        private readonly IAssignor _assignor;

        public SalespersonController(ISalespersonRepository salesRepo, IAssignor assignor)
        {
            _salesRepo = salesRepo;
            _assignor = assignor;
        }

        [HttpPost]
        public Result Assign(CustomerFormDTO form)
        {
            Result dto = _assignor.AssignSalesperson(form);
            // _writer.SetSalespersonToBusy(dto.assignedSalesperson);
            return dto;
        }
    }
}
