using System;
using System.Threading;
using System.Threading.Tasks;
using Shiny.Jobs;

namespace ConferenceApp.Services
{
    public class BackgroundSyncJob : IJob
    {
        private readonly ISyncService syncService;

        public BackgroundSyncJob(ISyncService syncService)
        {
            this.syncService = syncService;
        }

        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancellationToken)
        {
            Console.WriteLine("Syncing conference data in the background...");
            var result = await syncService.SyncConferenceData(cancellationToken);
            Console.WriteLine($"Finished with result: {result}");

            jobInfo.Repeat = true;

            return result;
        }
    }
}
