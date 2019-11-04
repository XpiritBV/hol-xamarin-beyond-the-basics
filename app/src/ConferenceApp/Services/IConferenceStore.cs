using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;

namespace ConferenceApp.Services
{
    public interface IConferenceStore
    {
        Task<IEnumerable<Session>> GetSessions();
        Task<IEnumerable<Speaker>> GetSpeakers();
        Task<IEnumerable<Session>> GetSessionsForSpeaker(Guid speakerId);
    }
}
