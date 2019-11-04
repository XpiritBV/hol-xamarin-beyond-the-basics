using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;
using Newtonsoft.Json;

namespace ConferenceApp.Services
{
    public class ConferenceStore : IConferenceStore
    {
        private const string BASE_URI = @"https://oganize-api-endpoint.azurewebsites.net";
        private const string SESSIONS_PATH = @"/api/events/5351926f-c8cd-4498-8c85-9cdb63257c80/sessions";

        private static readonly HttpClient httpClient = new HttpClient { BaseAddress = new System.Uri(BASE_URI) };

        public async Task<IEnumerable<Session>> GetSessions()
        {
            var response = await httpClient.GetAsync(SESSIONS_PATH).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return await Task.Run(() =>
                {
                    var sessions = JsonConvert.DeserializeObject<IEnumerable<Session>>(json);
                    return sessions;
                });
            }

            return Enumerable.Empty<Session>();
        }

        public async Task<IEnumerable<Speaker>> GetSpeakers()
        {
            // speakers come from the same data source, we just need to pluck it from the sessions
            var response = await httpClient.GetAsync(SESSIONS_PATH).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return await Task.Run(() =>
                {
                    var sessions = JsonConvert.DeserializeObject<IEnumerable<Session>>(json);

                    return sessions.SelectMany(s => s.Speakers)
                                    .Distinct(new SpeakerComparer())
                                    .OrderBy(s => s.FirstName)
                                    .ThenBy(s => s.LastName)
                                    .ToList();
                });
            }

            return Enumerable.Empty<Speaker>();
        }

        private class SpeakerComparer : IEqualityComparer<Speaker>
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
