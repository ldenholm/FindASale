using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindASale.Models
{
    public class Salesperson
    {
        public string Name { get; set; }
        public List<char> Groups { get; set; }

        public Salesperson()
        {
            Groups = new List<char>();
        }

        // Could potentially leave above prop as List<string> as char 
        // includes some additional ASCII info when it assigned value from json db
    }
}
