using FindASale.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindASale.Models
{
    public class CustomerFormDTO
    {
        public bool SpeaksGreek { get; set; }
        public CarType CarType { get; set; }
        public CustomerFormDTO ConvertJson(string json)
        {
            var obj = JsonConvert.DeserializeObject<CustomerFormDTO>(json);
            return obj;
        }
    }
}
