using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Contracts.DTO
{
    public class EnvirontmentDTO
    {
        public string context { get; set; }
        public EnvironmentMetaDTO meta { get; set; }
        public List<EnvironmentMetaDTO> items { get; set; }
    }
}
