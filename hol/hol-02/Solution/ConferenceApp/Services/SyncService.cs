using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConferenceApp.Contracts;

namespace ConferenceApp.Services
{
    public class SyncService : ISyncService
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);

        private readonly IConferenceStore conferenceStore;
        private readonly IConferenceApiService conferenceService;

        public SyncService(IConferenceStore conferenceStore, IConferenceApiService conferenceService)
        {
            this.conferenceStore = conferenceStore;
            this.conferenceService = conferenceService;
        }

        public async Task SyncIfNecessary(CancellationToken cancellationToken)
        {
            try
            {
                // allow only one at a time
                await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
                Console.WriteLine("~~~SyncService: aquired sync semaphore");

                if (!conferenceStore.HasData())
                {
                    await SyncConferenceData(cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    Console.WriteLine("~~~SyncService: no need to sync with server");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"~~~SyncService: exception during sync: {ex}");
            }
            finally
            {
                Console.WriteLine("~~~SyncService: releasing sync semaphore");
                semaphore.Release();
            }
        }

        public async Task<bool> SyncConferenceData(CancellationToken cancellationToken)
        {
            Console.WriteLine("~~~SyncService: syncing conference data with server");

            var sessions = await conferenceService.DownloadConferenceData(cancellationToken).ConfigureAwait(false);

            if (!cancellationToken.IsCancellationRequested)
            {
                if (sessions.Any())
                {
                    await conferenceStore.ReplaceData(sessions).ConfigureAwait(false);
                    return true;
                }
            }

            return false;
        }
    }
}
