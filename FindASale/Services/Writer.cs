using FindASale.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FindASale.Services
{
    public interface IWriter
    {
        void UpdateAvailability(Salesperson person, bool isAvailable);
        ResetResult ResetAllToFalse();
    }
    public class Writer : IWriter
    {
        private readonly ISalespersonRepository _salesRepo;
        private readonly string _dbPath;
        public Writer(ISalespersonRepository salesRepo)
        {
            _salesRepo = salesRepo;
            _dbPath = Directory.GetCurrentDirectory() + "\\salesperson.json";
        }
        public void UpdateAvailability(Salesperson person, bool isAvailable)
        {
            var salespersonList = _salesRepo.LoadSalespersons();

            // Presume we always find the sales person here, for now the list is static so I am
            // not implementing error handling here. If this were to be extended I would add an
            // if to check that whether the list contains the person.

            salespersonList.Find(p => p.Name == person.Name).IsAvailable = isAvailable;

            using (StreamWriter file = File.CreateText(_dbPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, salespersonList);
            }
        }
        public ResetResult ResetAllToFalse()
        {
            try
            {
                // reset availability of all salespersons to true

                var list = _salesRepo.LoadSalespersons();

                list.ForEach(p => p.IsAvailable = true);

                using (StreamWriter file = File.CreateText(_dbPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, list);
                }
                return new ResetResult()
                {
                    ResetMessage = "Reset Successful"
                };
            }

            catch (Exception e)
            {
                return new ResetResult()
                {
                    ResetMessage = e.Message
                };
            }

        }
    }
}
