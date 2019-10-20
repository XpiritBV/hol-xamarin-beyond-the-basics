using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;

namespace ConferenceApp.Content.Speakers
{
    public interface ISpeakerStore
    {
        Task<ICollection<Speaker>> GetSpeakers();
    }
}