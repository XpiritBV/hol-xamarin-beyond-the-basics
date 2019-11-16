using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;
using Newtonsoft.Json;

namespace ConferenceApp.Services
{
    public class ConferenceApiService : IConferenceApiService
    {
        private const string BASE_URI = @"https://conferenceapp-demo.azurewebsites.net";
        private const string SESSIONS_PATH = @"/api/sessions";

        private static readonly HttpClient httpClient = new HttpClient { BaseAddress = new System.Uri(BASE_URI) };

        public async Task<IEnumerable<Session>> DownloadConferenceData(CancellationToken cancellationToken)
        {
            var response = await httpClient.GetAsync(SESSIONS_PATH, cancellationToken).ConfigureAwait(false);

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
    }
}
