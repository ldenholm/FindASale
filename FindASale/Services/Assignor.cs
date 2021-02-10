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
        //Result AssignGreek(CustomerFormDTO dto);
        //Result AssignSpecialist(CustomerFormDTO dto);
        Result AllocateSalesperson(List<char> groups);
        Result HandleSpecialist(List<char> groups);
        Result HandleGreekSpeaker(List<char> groups);
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

            else if (dto.Groups.Count == 0)
            {
                // No groups so pick one at random
                var availablePerson = _salesRepo.LoadSalespersons();
                return new Result()
                {
                    Success = true,
                    AssignedSalesPerson = ChooseRandom(availablePerson)
                };
            }

            else
            {
                // pass groups into function to find best salesperson
                return AllocateSalesperson(dto.Groups);
            }
        }

        public Result AllocateSalesperson(List<char> groups)
        {
            // see if char does not contain A speaking greek
            if (!groups.Contains('A'))
            {
                return HandleSpecialist(groups);
            }

            else
            {
                return HandleGreekSpeaker(groups);
            }
        }

        public Result HandleSpecialist(List<char> groups)
        {
            // use sales repo to find available specialist of that group, if none found choose at random
            if (!_salesRepo.SpecialistsAvailable(groups).Any())
            {
                // there are no specialists available for the group given, so choose one at random from the available
                var availableSalesperson = _salesRepo.GetAllAvailableSalespersons();
                return new Result()
                {
                    Success = true,
                    AssignedSalesPerson = ChooseRandom(availableSalesperson)
                };
            }
            else
            {
                return new Result()
                {
                    Success = true,
                    AssignedSalesPerson = _salesRepo.SpecialistsAvailable(groups).FirstOrDefault()
                };
            }
        }

        public Result HandleGreekSpeaker(List<char> groups)
        {
            if (!_salesRepo.GetGreekSalespersons().Any())
            {
                return HandleSpecialist(groups);
            }

            // if count of groups NOT greater than 1 look for specialist, otherwise return default
            if (groups.Count() < 2)
            {
                // has only 1 group so return default
                return new Result()
                {
                    Success = true,
                    AssignedSalesPerson = _salesRepo.GetGreekSalespersons().FirstOrDefault()
                };
            }
            // has multiple groups so get greek speaker and specialist
            // if no specialist that speaks greek
            if (!_salesRepo.GetGreekSalespersons().Where(p => p.Groups.Contains(groups[1])).Any())
            {
                // no specialists found that also speak greek so return random
                return new Result()
                {
                    Success = true,
                    AssignedSalesPerson = ChooseRandom(_salesRepo.GetAllAvailableSalespersons())
                };
            }

            // They want a greek speaker and a specialist, so get list of someone who does both and return first.
            return new Result()
            {
                Success = true,
                AssignedSalesPerson = _salesRepo.GetGreekSalespersons().Where(p => p.Groups.Contains(groups[1])).FirstOrDefault()
            };
        }

        public Salesperson ChooseRandom(IEnumerable<Salesperson> list)
        {
            Random rand = new Random();
            int index = rand.Next(0, list.Count() - 1);
            return list.ElementAt(index);
        }
    }
}
