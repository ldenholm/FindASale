using FindASale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindASale.Services
{
    public interface IAssignor
    {
        Result AssignSalesperson(CustomerFormDTO dto);
    }
    public class Assignor : IAssignor
    {
        private readonly ISalespersonRepository _salesRepo;

        public Assignor(ISalespersonRepository salesRepo)
        {
            _salesRepo = salesRepo;
        }

        public Result AssignSalesperson(CustomerFormDTO dto)
        {
            /* Logic to assign a sales person:
            * If a salesperson is assigned to a customer, that person 
            * cannot be assigned to another customer at the same time. If there are no salespeople available, 
            * the application should return a message saying, "All salespeople are busy. Please wait."
            */

            // Check if there are any available personnel
            if (!_salesRepo.GetAllAvailableSalespersons().Any())
            {
                // None available, return "All salespeople are busy. Please wait"
                return new Result()
                {
                    Success = false,
                    ErrorMessage = "All salespeople are busy. Please wait."
                };
            }

            // Else we have salespeople available, let's see if they speak Greek.
            if (dto.SpeaksGreek)
            {
                //
                var options = _salesRepo.GetGreekSalespersons();
            }
        }

        
    }
}
