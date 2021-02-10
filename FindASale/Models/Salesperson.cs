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
        public bool IsAvailable { get; set; }

        public Salesperson()
        {
            Groups = new List<char>();
        }
    }
}
