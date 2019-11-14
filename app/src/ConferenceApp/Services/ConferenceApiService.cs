using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;
using Polly;
using Polly.Retry;
using Refit;

namespace ConferenceApp.Services
{
    public class ConferenceApiService : IConferenceApiService
    {
        private readonly IConferenceApi conferenceApi;

        // Handles ApiExceptions with Http status codes >= 500 (server errors) and status code 408 (request timeout)
        private readonly AsyncRetryPolicy transientApiErrorPolicy = Policy
            .Handle<ApiException>(e => (int)e.StatusCode >= 500)
            .Or<ApiException>(e => e.StatusCode == HttpStatusCode.RequestTimeout)
            .WaitAndRetryAsync
            (
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            );

        public ConferenceApiService(IConferenceApi conferenceApi)
        {
            this.conferenceApi = conferenceApi;
        }

        public async Task<IEnumerable<Session>> DownloadConferenceData(CancellationToken cancellationToken)
        {
            return await transientApiErrorPolicy
                .ExecuteAsync(async () =>
                {
                    Debug.WriteLine("Trying service call...");
                    return await conferenceApi.GetSessions().ConfigureAwait(false);
                });
        }
    }
}
