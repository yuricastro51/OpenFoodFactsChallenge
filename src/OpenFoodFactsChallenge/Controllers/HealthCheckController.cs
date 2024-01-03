using Microsoft.AspNetCore.Mvc;

namespace OpenFoodFactsChallenge.Controllers
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        [Route("/")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult HealthCheck()
        {
            return Ok("Fullstack Challenge 20201026");
        }
    }
}
