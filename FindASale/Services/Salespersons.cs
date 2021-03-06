﻿using FindASale.Enums;
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
        IEnumerable<Salesperson> GetAllAvailableSalespersons(List<Salesperson> list);
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
        public IEnumerable<Salesperson> GetAllAvailableSalespersons(List<Salesperson> list)
        {
            return list.Where(p => p.IsAvailable == true);
        }

        public IEnumerable<Salesperson> GetGreekSalespersons()
        {
            var greekSalespeople = GetAllAvailableSalespersons(LoadSalespersons()).Where(p => p.Groups.Contains('A'));
            return greekSalespeople;
        }

        public IEnumerable<Salesperson> SpecialistsAvailable(List<char> groups)
        {
            return GetAllAvailableSalespersons(LoadSalespersons()).Where(p => p.Groups.Contains(groups[0]));
        }
    }
}
