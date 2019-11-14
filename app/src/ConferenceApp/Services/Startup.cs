using System.Net.Http;
using ConferenceApp.Contracts;
using Infrastructure.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Shiny;

namespace ConferenceApp.Services
{
    public class Startup : ShinyStartup
    {
        private const string BASE_URI = @"https://conferenceapp-demo.azurewebsites.net/api";

        private static readonly HttpClient httpClient = new HttpClient { BaseAddress = new System.Uri(BASE_URI) };

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(p => RestService.For<IConferenceApi>(httpClient));
            services.AddSingleton<IConferenceApiService, ConferenceApiService>();
            services.AddSingleton<IConferenceStore, ConferenceSqliteStore>();
            services.AddSingleton<ISyncService, SyncService>();
        }
    }
}
