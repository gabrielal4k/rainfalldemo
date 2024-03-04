using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Contracts.DTO
{
    public class RainfallReadingDTO
    {
        //this class is for the return data for success

        public DateTime dateMeasured { get; set; }
        public decimal amountMeasured { get; set; }
    }
}
