using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceApp.Contracts
{
    public interface IConferenceStore
    {
        bool HasData();

        Task ReplaceData(IEnumerable<Models.Session> sessions);

        Task FavoriteSession(string sessionId);
        Task UnfavoriteSession(string sessionId);

        Task<Models.Session> GetSession(string sessionId);
        Task<IEnumerable<Models.Session>> GetSessions();
        Task<IEnumerable<Models.Session>> GetSessionsForSpeaker(Guid speakerId);

        Task<Models.Speaker> GetSpeaker(Guid speakerId);
        Task<IEnumerable<Models.Speaker>> GetSpeakers();
    }
}
