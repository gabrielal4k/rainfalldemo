using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Contracts.DTO
{
    public class EnvironmentMetaDTO
    {
        public string publisher { get; set; }
        public string licence { get; set; }
        public string documentation { get; set; }
        public float version { get; set; }
        public string comment { get; set; }
        public List<string> hasFormat { get; set; }
        public int limit { get; set; }
    }
}
