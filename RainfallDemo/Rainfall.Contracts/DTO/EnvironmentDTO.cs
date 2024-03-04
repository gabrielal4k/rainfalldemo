using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Contracts.DTO
{
    public class EnvirontmentDTO
    {
        //this data transfer object are for containing the return values from api

        public string context { get; set; }
        public EnvironmentMetaDTO meta { get; set; }
        public List<EnvironmentItemDTO> items { get; set; }
    }
}
