﻿using FindASale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindASale.Services
{
    public interface IAgent
    {
        Result ProcessAssignment(CustomerFormDTO formData);
    }
    public class Agent : IAgent
    {
        private readonly IAssignor _assignor;
        private readonly IWriter _writer;

        public Agent(IAssignor assignor, IWriter writer)
        {
            _assignor = assignor;
            _writer = writer;
        }

        public Result ProcessAssignment(CustomerFormDTO formData)
        {
            Result dto = _assignor.AssignSalesperson(formData);
            _writer.UpdateAvailability(dto.AssignedSalesPerson, false);

            return dto;
        }
    }
}