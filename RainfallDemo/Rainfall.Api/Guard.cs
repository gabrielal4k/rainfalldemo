using Rainfall.Contracts.DTO;
using System.Runtime.CompilerServices;

namespace Rainfall.Api
{
    public class Guard
    {
        private ResultResponse CreateResponse(string statusText, bool error = false, int responseState = 200, object data = null )
        {
            return new ResultResponse()
            {
                Error = error,
                ResponseState = responseState,
                StatusText = statusText,
                Data = data
            };
        }

        public ResultResponse CheckStationID(int stationID)
        {
            //a check method to check if the station ID is within the range of acceptable value.

            if (stationID < 1 || stationID > 100)
                return CreateResponse("Invalid request", true, 400);

            return CreateResponse("Ok", false, 200);
        }

      
    }
}
