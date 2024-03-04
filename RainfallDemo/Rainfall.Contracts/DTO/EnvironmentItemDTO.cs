using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Contracts.DTO
{
    public class EnvironmentItemDTO
    {
        public string id { get; set; }
        public string dateTime { get; set; }
        public string measure { get; set; }
        public decimal value { get; set; }
    }
}
