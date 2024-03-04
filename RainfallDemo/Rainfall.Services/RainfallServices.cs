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
        //this is the service layer of the app. this is where I handle logical functions of related methods and objects for this app.

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
                var response = await _api.GetStationsReading(stationID);

                if (response.Error)
                    return response;

                EnvirontmentDTO dto = (EnvirontmentDTO)response.Data;

                if (dto.items.Count == 0)
                    return response = new ResultResponse()
                    {
                        ResponseState = 204,
                        StatusText = "No Content",
                        Error = true
                    };

                //get latest rainfall  data if there is then sort the needed api data for the required return data by looping.
                //calling each items of the list of data then assigning each needed values for the new data.
                List<RainfallReadingDTO> rainfallDTOs = new List<RainfallReadingDTO>();
                foreach (var item in dto.items)
                {
                    RainfallReadingDTO rainfallDTO = new()
                    {
                        dateMeasured = DateTime.Parse(item.dateTime),
                        amountMeasured = item.value
                    };

                    rainfallDTOs.Add(rainfallDTO);
                }

                response.Data = rainfallDTOs.OrderByDescending(x => x.dateMeasured).ToArray();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("ServiceLayer: RetrieveRainfallData" + ex.GetBaseException().Message);
            }
        }

    }
}
