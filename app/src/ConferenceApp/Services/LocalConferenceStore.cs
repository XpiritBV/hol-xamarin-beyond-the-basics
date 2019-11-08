using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;
using ConferenceApp.Helpers;
using Newtonsoft.Json;

namespace ConferenceApp.Services
{
    public class LocalConferenceStore : IConferenceStore
    {
       public async Task<IEnumerable<Session>> GetSessions()
        {
            string json = GetLocalJsonStore();

            return await Task.Run(() =>
            {
                var sessions = JsonConvert.DeserializeObject<IEnumerable<Session>>(json);
                return sessions;
            });
        }

        private static string GetLocalJsonStore()
        {
            return ResourceLoader.GetEmbeddedResourceString(Assembly.GetExecutingAssembly(), "LocalConferenceStore.json");
        }

        public async Task<IEnumerable<Session>> GetSessionsForSpeaker(Guid speakerId)
        {
            // TODO: this is not optimal, need a way to find the sessions that
            // belong to a speaker only
            var sessions = await GetSessions();
            return sessions
                .Where(s => s.Speakers.Any(s => s.Id == speakerId))
                .ToList();
        }

        public async Task<IEnumerable<Speaker>> GetSpeakers()
        {
            // speakers come from the same data source, we just need to pluck it from the sessions
            var json = GetLocalJsonStore();

            return await Task.Run(() =>
            {
                var sessions = JsonConvert.DeserializeObject<IEnumerable<Session>>(json);

                return sessions.SelectMany(s => s.Speakers)
                                .Distinct(new LocalSpeakerComparer())
                                .OrderBy(s => s.FirstName)
                                .ThenBy(s => s.LastName)
                                .ToList();
            });
           
        }

        private class LocalSpeakerComparer : IEqualityComparer<Speaker>
        {
            public bool Equals(Speaker x, Speaker y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Speaker obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
