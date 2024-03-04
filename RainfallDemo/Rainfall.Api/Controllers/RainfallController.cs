using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rainfall.Api.Controllers
{
    [ApiController]
    public class RainfallController : Controller
    {

        public RainfallController()
        {

        }

        [HttpGet, Route("/api"), AllowAnonymous]
        public IActionResult GetRainfall_By_StationID(int stationId)
        {
            return default;
        }

    }
}
