using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;

namespace ConferenceApp.Content.Sessions
{
    public interface ISessionStore
    {
        Task<ICollection<Session>> GetSessions();
    }
}
