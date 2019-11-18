using System;
using System.Collections.Generic;
using ConferenceWeb.Data;

namespace ConferenceWeb.Models
{
    public class SpeakersViewModel
    {
        public SpeakersViewModel()
        {
        }

        public IEnumerable<Speaker> Speakers { get; set; }
    }
}
