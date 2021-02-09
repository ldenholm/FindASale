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
                if (!_salesRepo.GreekAndSpecialistExists(salesPersonnel))
                {
                    // none exists so choose random
                    return new Result()
                    {
                        Success = true,
                        AssignedSalesPerson = ChooseRandom(salesPersonnel)
                    };
                }
                else
                {
                    // there is a specialist who speaks greek so find first result
                    return new Result()
                    {
                        Success = true,
                        AssignedSalesPerson = _salesRepo.UnionGreekAndSpecialist(salesPersonnel).FirstOrDefault()
                    };
                }
            }
            else
            {
                // dont speak greek so return specialist
                return new Result()
                {
                    Success = true,
                    AssignedSalesPerson = salesPersonnel.FirstOrDefault()
                };
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
