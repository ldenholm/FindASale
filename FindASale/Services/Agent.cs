using FindASale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindASale.Services
{
    public interface IAgent
    {
        Result ProcessAssignment(CustomerFormDTO formData);
        ResetResult ResetAvailability();
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
            if (dto.AssignedSalesPerson != null)
            {
                _writer.UpdateAvailability(dto.AssignedSalesPerson, false);
            }

            return dto;
        }

        public ResetResult ResetAvailability()
        {
            return _writer.ResetAllToFalse();
        }
    }
}
