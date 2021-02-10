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
        List<Salesperson> LoadSalespersons();
        Salesperson LoadSalespersonByID(string name);
        IEnumerable<Salesperson> GetAllAvailableSalespersons();
        IEnumerable<Salesperson> GetGreekSalespersons();
        IEnumerable<Salesperson> SpecialistsAvailable(List<char> groups);
    }
    public class SalespersonsRepository : ISalespersonRepository
    {
        private readonly string _dbPath;

        public SalespersonsRepository()
        {
            _dbPath = Directory.GetCurrentDirectory() + "\\salesperson.json";
        }
        public List<Salesperson> LoadSalespersons()
        {
            var list = JsonConvert.DeserializeObject<List<Salesperson>>(File.ReadAllText(_dbPath));

            return list;
        }
        public Salesperson LoadSalespersonByID(string name)
        {
            var salesperson = LoadSalespersons().Where(p => p.Name == name).FirstOrDefault();
            return salesperson;
        }
        public IEnumerable<Salesperson> GetAllAvailableSalespersons()
        {
            var availablePersonnel = LoadSalespersons().Where(p => p.IsAvailable = true);
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

        public IEnumerable<Salesperson> SpecialistsAvailable(List<char> groups)
        {
            return GetAllAvailableSalespersons().Where(p => p.Groups.Contains(groups[0]));
        }
    }
}
