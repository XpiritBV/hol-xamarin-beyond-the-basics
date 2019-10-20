using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;

// remove after implementing real services
using Test.FakeData;

namespace ConferenceApp.Content.Sessions
{
    public class SessionStore : ISessionStore
    {
        private readonly ICollection<Session> sessions;

        public SessionStore()
        {
            var speakerFaker = new SpeakerFaker();

            var faker = new SessionFaker().WithSpeakers(speakerFaker.Generate(100).ToArray());

            sessions = faker.Generate(150).ToArray();
        }

        public async Task<ICollection<Session>> GetSessions()
        {
            await Task.Delay(1000); // simulate waiting time until we implement real HTTP calls

            return sessions;
        }
    }
}
