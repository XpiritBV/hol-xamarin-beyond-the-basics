using System.Linq;
using System.Net;
using ConferenceWeb.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConferenceWeb.Api
{
    [Route("api/[controller]")]
    public class SessionsController : Controller
    {
        [HttpGet]
        public IActionResult Get([FromQuery] bool injectError = false)
        {
            if (injectError)
            {
                return StatusCode((int) HttpStatusCode.BadGateway, "Bad gateway :(");
            }
            return Ok(DataStore.Sessions);
        }

        [HttpGet("{sessionId}")]
        public IActionResult Get([FromRoute] string sessionId)
        {
            var session = DataStore.Sessions.FirstOrDefault(s => s.Id == sessionId);

            if (session != null)
            {
                return Ok(session);
            }
            return NotFound();
        }
    }
}
