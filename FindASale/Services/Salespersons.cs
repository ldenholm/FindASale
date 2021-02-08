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
        List<Salesperson> GetAllSalespersons();
    }
    public class SalespersonsRepository : ISalespersonRepository
    {
        private readonly string _databasePath;
        //private SalespersonList _salespersonsList;

        public SalespersonsRepository()
        {
            _databasePath = Directory.GetCurrentDirectory() + "\\salesperson.json";
        }
        public List<Salesperson> GetAllSalespersons()
        {
            var list = JsonConvert.DeserializeObject<List<Salesperson>>(File.ReadAllText(_databasePath));

            return list;
        }
    }
}
