using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;

namespace ConferenceApp.Services
{
    public interface IConferenceApiService
    {
        Task<IEnumerable<Session>> DownloadConferenceData(CancellationToken cancellationToken);
    }
}
