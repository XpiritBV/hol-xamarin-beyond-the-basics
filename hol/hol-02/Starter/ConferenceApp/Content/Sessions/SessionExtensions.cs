using System.Collections.Generic;
using ConferenceApp.Contracts.Models;
using System.Linq;
using System;

namespace ConferenceApp.Content.Sessions
{
    public static class SessionExtensions
    {
        public static IList<SessionGroup> GroupByStartTime(this IEnumerable<Session> sessions)
        {
            var grouped = (from session in sessions
                           orderby session.StartsAt, session.Title
                           group session by session.StartsAt
                           into sessionGroup
                           select new SessionGroup(sessionGroup.Key, sessionGroup.ToList()));

            return grouped.ToList();
        }
    }
}
