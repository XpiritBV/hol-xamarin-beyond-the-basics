using System;
using System.Collections.Generic;
using ConferenceApp.Contracts.Models;

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

                var now = DateTimeOffset.UtcNow;

                if (now.Year == StartTime.Year)
                {
                    if (now.DayOfYear == StartTime.DayOfYear)
                        return $"Today {startString}";

                    if (now.DayOfYear + 1 == StartTime.DayOfYear)
                        return $"Tomorrow {startString}";
                }
                var day = StartTime.ToString("M");
                return $"{day}, {startString}";
            }
        }
    }
}
