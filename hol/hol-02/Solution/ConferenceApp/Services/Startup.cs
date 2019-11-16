using ConferenceApp.Contracts;
using Infrastructure.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Shiny;

namespace ConferenceApp.Services
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConferenceApiService, ConferenceApiService>();
            services.AddSingleton<IConferenceStore, ConferenceSqliteStore>();
            services.AddSingleton<ISyncService, SyncService>();
        }
    }
}
