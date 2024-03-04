using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Contracts.DTO
{
    internal class RainfallReadingDTO
    {
        public DateTime dateMeasured { get; set; }
        public decimal amountMeasured { get; set; }
    }
}
