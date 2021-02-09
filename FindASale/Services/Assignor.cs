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
        Salesperson ChooseRandom(IEnumerable<Salesperson> list);
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

            // find the car type the customer likes
            var salesPersonnel = _salesRepo.SpecialistSwitch(dto.CarType);

            if (dto.SpeaksGreek)
            {
                // squash list to only include those speaking Greek, if none found assign randomly:
                var GreekAndSpecialistList = salesPersonnel.Union(_salesRepo.GetGreekSalespersons()).Distinct();

                // if there are none in the union, then plug it through random function
                if (!GreekAndSpecialistList.Any())
                {
                    return new Result()
                    {
                        Success = true,
                        AssignedSalesPerson = ChooseRandom(salesPersonnel)
                    };
                }
                else
                {
                    return new Result()
                    {
                        Success = true,
                        AssignedSalesPerson = GreekAndSpecialistList.FirstOrDefault()
                    };
                }
            }
        }

        public Salesperson ChooseRandom(IEnumerable<Salesperson> list)
        {
            Random rand = new Random();
            int index = rand.Next(0, list.Count() - 1);
            return list.ElementAt(index);
        }
    }
}
