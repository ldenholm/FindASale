using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindASale.Models
{
    public class SalespersonDTO
    {
        public bool Success { get; set; }
        public Salesperson AssignedSalesPerson { get; set; }
        public Dictionary<int, string> ErrorMessages { get; set; }

        public SalespersonDTO()
        {
            ErrorMessages = new Dictionary<int, string>();
        }
    }
}
