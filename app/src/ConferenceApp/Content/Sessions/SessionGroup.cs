using System;
using System.Collections.Generic;
using ConferenceApp.Contracts.Models;
using ConferenceApp.Services;

namespace ConferenceApp.Content.Sessions
{
    public class SessionGroup : List<Session>
    {
        public SessionGroup(DateTimeOffset startTime, List<Session> sessions) : base (sessions)
        {
            this.StartTime = startTime;
        }

        public DateTimeOffset StartTime { get; }

        public string GroupName
        {
            get
            {
                var startString = StartTime.ToString("t");

                if (Clock.Now.Year == StartTime.Year)
                {
                    if (Clock.Now.DayOfYear == StartTime.DayOfYear)
                        return $"Today {startString}";

                    if (Clock.Now.DayOfYear + 1 == StartTime.DayOfYear)
                        return $"Tomorrow {startString}";
                }
                var day = StartTime.ToString("M");
                return $"{day}, {startString}";
            }
        }
    }
}
