using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Contracts.DTO
{
    public class ResultResponse
    {
        public ResultResponse()
        {
            Error = false;
            StatusText = string.Empty;
            Data = null;
            ResponseState = 200;
        }

        public string? StatusText { get; set; }
        public object? Data { get; set; }
        public Boolean Error { get; set; }
        public int ResponseState { get; set; }
    }
}
