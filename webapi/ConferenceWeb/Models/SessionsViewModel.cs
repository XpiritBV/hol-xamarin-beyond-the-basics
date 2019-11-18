using System;
using System.Collections;
using System.Collections.Generic;
using ConferenceWeb.Data;

namespace ConferenceWeb.Models
{
    public class SessionsViewModel
    {
        public SessionsViewModel()
        {
        }
        
        public IEnumerable<Session> Sessions { get; set; }
    }
}
