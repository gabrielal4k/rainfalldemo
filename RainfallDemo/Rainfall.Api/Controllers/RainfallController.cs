using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Rainfall.Contracts.DTO;
using Rainfall.Contracts.Interface;

namespace Rainfall.Api.Controllers
{
    [ApiController]
    public class RainfallController : Controller
    {
        Guard _guard = new Guard();
        IRainfallServices _service;

        public RainfallController(IRainfallServices src)
        {
            _service = src;
        }

        [HttpGet, Route("/rainfall/{stationId}/station"), AllowAnonymous]
        public async Task<ActionResult< List<RainfallReadingDTO>> > GetRainfall_By_StationID(int stationId)
        {
            try
            {
                var response =  _guard.CheckStationID(stationId);

                if(response.Error)
                    return ObjectResultHander(response);

                response = await _service.RetrieveRainfallData(stationId);

                return ObjectResultHander(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private ObjectResult ObjectResultHander(ResultResponse response)
        {
            //a method to return status object based on status code. it will return http status based on their respective code ie: 200 = OK

            switch (response.ResponseState)
            {
                case 200:
                    return StatusCode(response.ResponseState, response.Data);
                case 204:
                    return StatusCode(response.ResponseState, response.StatusText);
                default:
                    var result = $"{response.StatusText} {response.Data}";
                    return StatusCode(response.ResponseState, result);
            }

        }
             
    }
}
