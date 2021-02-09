using FindASale.Enums;
using FindASale.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FindASale.Services
{
    public interface ISalespersonRepository
    {
        //Task<List<Salesperson>> GetAllSalespersonsAsync();
        List<Salesperson> LoadSalespersons();
        IEnumerable<Salesperson> GetAllAvailableSalespersons();
        IEnumerable<Salesperson> GetGreekSalespersons();
        IEnumerable<Salesperson> GetFamilyCarSpecialists();
        IEnumerable<Salesperson> GetSportsCarSpecialists();
        IEnumerable<Salesperson> GetTradieSpecialists();
        IEnumerable<Salesperson> SpecialistSwitch(CarType cartype);
        IEnumerable<Salesperson> UnionGreekAndSpecialist(IEnumerable<Salesperson> greekList, CarType carType);
        bool GreekAndSpecialistExists(IEnumerable<Salesperson> greekList, CarType carType);
    }
    public class SalespersonsRepository : ISalespersonRepository
    {
        private readonly string _databasePath;
        //private SalespersonList _salespersonsList;

        public SalespersonsRepository()
        {
            _databasePath = Directory.GetCurrentDirectory() + "\\salesperson.json";
        }
        public List<Salesperson> LoadSalespersons()
        {
            var list = JsonConvert.DeserializeObject<List<Salesperson>>(File.ReadAllText(_databasePath));

            return list;
        }

        public IEnumerable<Salesperson> GetAllAvailableSalespersons()
        {
            var availablePersonnel = LoadSalespersons().Where(p => p.IsAvailable == true);
            return availablePersonnel;
        }

        public IEnumerable<Salesperson> GetGreekSalespersons()
        {
            var greekSalespeople = GetAllAvailableSalespersons().Where(p => p.Groups.Contains('A'));
            return greekSalespeople;
        }

        public IEnumerable<Salesperson> GetSportsCarSpecialists()
        {
            var sportsSpecialists = GetAllAvailableSalespersons().Where(p => p.Groups.Contains('B'));
            return sportsSpecialists;
        }

        public IEnumerable<Salesperson> GetFamilyCarSpecialists()
        {
            var familySpecialists = GetAllAvailableSalespersons().Where(p => p.Groups.Contains('C'));
            return familySpecialists;
        }

        public IEnumerable<Salesperson> GetTradieSpecialists()
        {
            var tradieSpecialists = GetAllAvailableSalespersons().Where(p => p.Groups.Contains('D'));
            return tradieSpecialists;
        }

        public IEnumerable<Salesperson> SpecialistSwitch(CarType carType)
        {
            switch (carType)
            {
                case CarType.FamilyCars:
                    return GetFamilyCarSpecialists();

                case CarType.SportsCars:
                    return GetSportsCarSpecialists();

                case CarType.TradieVehicles:
                    return GetTradieSpecialists();

                default:
                    return GetAllAvailableSalespersons();
            }
        }

        public IEnumerable<Salesperson> UnionGreekAndSpecialist(IEnumerable<Salesperson> greekList, CarType carType)
        {
            var combinedList = greekList.Union(SpecialistSwitch(carType).Distinct());
            return combinedList;
        }

        public bool GreekAndSpecialistExists(IEnumerable<Salesperson> greekList, CarType carType)
        {
            var doesExist = greekList.Union(SpecialistSwitch(carType)).Distinct().Any();
            return doesExist;
        }
    }
}
