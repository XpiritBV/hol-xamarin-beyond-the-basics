using System;
using System.Linq;
using ConferenceWeb.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConferenceWeb.Api
{
    [Route("api/[controller]")]
    public class SpeakersController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DataStore.Speakers);
        }

        [HttpGet("{speakerId}")]
        public IActionResult Get([FromRoute] Guid speakerId)
        {
            var speaker = DataStore.Speakers.FirstOrDefault(s => s.Id == speakerId);

            if (speaker != null)
            {
                return Ok(speaker);
            }
            return NotFound();
        }
    }
}
