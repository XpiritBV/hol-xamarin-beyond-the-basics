using System.Collections.Generic;
using ConferenceApp.Contracts.Models;
using ConferenceApp.Services;
using System.Linq;
using MvvmHelpers;
using System;

namespace ConferenceApp.Content.Sessions
{
    public static class SessionExtensions
    {
        public static string GetGroupName(this Session session)
        {
            var start = session.StartsAt;
            var startString = start.ToString("t");

            if (Clock.Now.Year == start.Year)
            {
                if (Clock.Now.DayOfYear == start.DayOfYear)
                    return $"Today {startString}";

                if (Clock.Now.DayOfYear + 1 == start.DayOfYear)
                    return $"Tomorrow {startString}";
            }
            var day = start.ToString("M");
            return $"{day}, {startString}";
        }

        public static IList<SessionGroup> FilterAndGroupByDate(this ICollection<Session> sessions, DateTime referenceDate)
        {
            //is not tba
            //has not started or has started and hasn't ended or ended 20 minutes ago
            //filter then by category and filters
            var grouped = (from session in sessions
                           orderby session.StartsAt, session.Title
                           group session by session.StartsAt
                           into sessionGroup
                           select new SessionGroup(sessionGroup.Key, sessionGroup.ToList()));

            return grouped.ToList();
        }

        public static IList<SessionGroup> FilterAndGroupByDate(this ICollection<Session> sessions)
        {
            return FilterAndGroupByDate(sessions, Clock.Now);
        }
    }
}
