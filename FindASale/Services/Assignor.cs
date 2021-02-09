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
        Result AssignGreek(CustomerFormDTO dto);
        Result AssignSpecialist(CustomerFormDTO dto);
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

            else if (dto.SpeaksGreek)
            {
                return AssignGreek(dto);
            }

            else
            {
                return AssignSpecialist(dto);
            }
        }

        public Result AssignGreek(CustomerFormDTO dto)
        {
            if (!_salesRepo.GetGreekSalespersons().Any())
            {
                return AssignSpecialist(dto);
            }
            else
            {
                // there are greek salespeople so lets union them with specialist
                var greekSales = _salesRepo.GetGreekSalespersons();
                if (!_salesRepo.GreekAndSpecialistExists(greekSales, dto.CarType))
                {
                    // choose random
                    return new Result()
                    {
                        Success = true,
                        AssignedSalesPerson = ChooseRandom(greekSales)
                    };
                }
                else
                {
                    return new Result()
                    {
                        Success = true,
                        AssignedSalesPerson = _salesRepo.UnionGreekAndSpecialist(greekSales, dto.CarType).FirstOrDefault()
                    };
                }

            }
        }

        public Result AssignSpecialist(CustomerFormDTO dto)
        {
            // Logic to assign specialist
            if (!_salesRepo.SpecialistSwitch(dto.CarType).Any())
            {
                var available = _salesRepo.GetAllAvailableSalespersons();
                // return random
                return new Result()
                {
                    Success = true,
                    AssignedSalesPerson = ChooseRandom(available)
                };
            }
            else
            {
                var specialists = _salesRepo.SpecialistSwitch(dto.CarType);
                return new Result()
                {
                    Success = true,
                    AssignedSalesPerson = specialists.FirstOrDefault()
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
