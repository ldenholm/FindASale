﻿using FindASale.Models;
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

        public SalespersonController(ISalespersonRepository salesRepo)
        {
            _salesRepo = salesRepo;
        }

        [HttpPost]
        public SalespersonDTO Assign(CustomerFormDTO form)
        {
            var list = _salesRepo.GetAllSalespersons();

            var dto = new SalespersonDTO()
            {
                Success = true,
                AssignedSalesPerson = list.FirstOrDefault()
            };

            return dto;
        }
    }
}