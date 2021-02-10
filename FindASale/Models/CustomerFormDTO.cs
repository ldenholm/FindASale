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
        public List<char> Groups { get; set; }
        public CustomerFormDTO ConvertJson(string json)
        {
            Groups = new List<char>();
            var obj = JsonConvert.DeserializeObject<CustomerFormDTO>(json);
            return obj;
        }
    }
}
