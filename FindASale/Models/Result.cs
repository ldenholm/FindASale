using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindASale.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public Salesperson AssignedSalesPerson { get; set; }
        public string ErrorMessage { get; set; }

        public Result()
        {
        }
    }

    public class ResetResult
    {
        public string ResetMessage { get; set; }
    }
}
