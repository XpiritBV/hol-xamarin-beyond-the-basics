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
            services.UseNotifications();

            services.AddSingleton(p => RestService.For<IConferenceApi>(httpClient));
            services.AddSingleton<IConferenceApiService, ConferenceApiService>();
            services.AddSingleton<IConferenceStore, ConferenceSqliteStore>();
            services.AddSingleton<ISyncService, SyncService>();

            services.RegisterJob(new Shiny.Jobs.JobInfo
            {
                Identifier = nameof(BackgroundSyncJob),
                Type = typeof(BackgroundSyncJob),
                RequiredInternetAccess = Shiny.Jobs.InternetAccess.Any,
                BatteryNotLow = true,
                Repeat = true
            });
        }


    }
}
