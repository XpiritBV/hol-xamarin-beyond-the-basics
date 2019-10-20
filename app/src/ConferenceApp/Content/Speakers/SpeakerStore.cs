using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;

// TODO: remove after adding real HTTP endpoints
using Test.FakeData;

namespace ConferenceApp.Content.Speakers
{
    public class SpeakerStore : ISpeakerStore
    {
        private readonly ICollection<Speaker> speakers;

        public SpeakerStore()
        {
            var speakerFaker = new SpeakerFaker();
            speakers = speakerFaker.Generate(150).ToArray();
        }

        public async Task<ICollection<Speaker>> GetSpeakers()
        {
            await Task.Delay(1000); // simulate waiting time until we implement real HTTP calls
            return speakers;
        }
    }
}
