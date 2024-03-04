using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Contracts.DTO
{
    public class EnvironmentItemDTO
    {
        public int id { get; set; }
        public DateTime dateTime { get; set; }
        public string measure { get; set; }
        public decimal value { get; set; }
    }
}
