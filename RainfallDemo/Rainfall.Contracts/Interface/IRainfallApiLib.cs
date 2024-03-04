using Rainfall.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Contracts.Interface
{
    public interface IRainfallApiLib
    {
        Task<ResultResponse> GetStationsReading(int id);
    }
}
