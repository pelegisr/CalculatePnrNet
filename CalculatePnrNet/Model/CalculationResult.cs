using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peleg.CalculatePnrNet.Model
{
    public class CalculationResult
    {
        public bool Success { get; set; }
        public decimal Value { get; set; }
        public string ErrorMessage { get; set; }
    }

}
