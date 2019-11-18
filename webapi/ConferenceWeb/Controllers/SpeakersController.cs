using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceWeb.Data;
using ConferenceWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConferenceWeb.Controllers
{
    public class SpeakersController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var vm = new SpeakersViewModel { Speakers = DataStore.Speakers.ToList() };

            return View(vm);
        }

        // GET: /<controller>/SpeakerDetail?speakerId=
        public IActionResult SpeakerDetail([FromQuery] Guid speakerId)
        {
            var speaker = DataStore.Speakers.FirstOrDefault(s => s.Id == speakerId);

            if (speaker != null)
            {
                var vm = new SpeakerDetailViewModel { Speaker = speaker };
                return View(vm);
            }

            return NotFound();
        }
    }
}
