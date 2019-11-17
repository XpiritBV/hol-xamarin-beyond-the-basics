using System;
using System.Threading;
using System.Threading.Tasks;
using Shiny.Jobs;
using Shiny.Notifications;

namespace ConferenceApp.Services
{
    public class BackgroundSyncJob : IJob
    {
        private readonly ISyncService syncService;
        private readonly INotificationManager notificationManager;

        public BackgroundSyncJob(ISyncService syncService, INotificationManager notificationManager)
        {
            this.syncService = syncService;
            this.notificationManager = notificationManager;
        }

        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancellationToken)
        {
            Console.WriteLine("Syncing conference data in the background...");
            var result = await syncService.SyncConferenceData(cancellationToken);
            Console.WriteLine($"Finished with result: {result}");

            await notificationManager.Send(new Notification { Message = "Your data was synced in the background", Title = "Sync" }).ConfigureAwait(false);

            jobInfo.Repeat = true;

            return result;
        }
    }
}
