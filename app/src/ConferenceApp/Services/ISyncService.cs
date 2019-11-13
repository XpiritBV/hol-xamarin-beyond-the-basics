using System.Threading;
using System.Threading.Tasks;

namespace ConferenceApp.Services
{
    public interface ISyncService
    {
        Task<bool> SyncConferenceData(CancellationToken cancellationToken);
        Task SyncIfNecessary(CancellationToken cancellationToken);
    }
}
