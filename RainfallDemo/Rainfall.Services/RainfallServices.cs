using Rainfall.Contracts.DTO;
using Rainfall.Contracts.Interface;
using Rainfall.Services.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Services
{
    public class RainfallServices : IRainfallServices
    {
        IRainfallApiLib _api;
        public RainfallServices(IRainfallApiLib src)
        {
            _api = src;
        }

        public async Task<ResultResponse> RetrieveRainfallData(int stationID)
        {
            try
            {
                Ensure.NotZero(stationID);
                var result = await _api.GetStationsReading(stationID);

                return default;
            }
            catch (Exception ex)
            {
                throw new Exception("ServiceLayer: RetrieveRainfallData" + ex.GetBaseException().Message);
            }
        }

    }
}
