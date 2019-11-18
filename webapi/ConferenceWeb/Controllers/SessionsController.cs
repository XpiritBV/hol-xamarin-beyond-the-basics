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
    public class SessionsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var vm = new SessionsViewModel { Sessions = DataStore.Sessions.ToList() };

            return View(vm);
        }

        // GET: /<controller>/SessionDetail?id=
        public IActionResult SessionDetail([FromQuery] string sessionId)
        {
            var session = DataStore.Sessions.FirstOrDefault(s => s.Id == sessionId);

            if (session != null)
            {
                var vm = new SessionDetailViewModel { Session = session };
                return View(vm);
            }

            return NotFound();
        }
    }
}
