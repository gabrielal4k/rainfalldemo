using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<ResultResponse>> GetRainfall_By_StationID(int stationId)
        {
            try
            {
                var response =  _guard.CheckStationID(stationId);

                if(response.Error)
                    return ErrorPasssHandler(response);

                response = await _service.RetrieveRainfallData(stationId);

                return response;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private ActionResult ErrorPasssHandler(ResultResponse response)
        {
            return StatusCode(500);
        }
             
    }
}
