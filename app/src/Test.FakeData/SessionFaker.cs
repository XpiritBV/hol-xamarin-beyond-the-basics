using System.Collections.Generic;
using Bogus;
using System.Linq;
using ConferenceApp.Contracts.Models;

namespace Test.FakeData
{
    public class SessionFaker : Faker<Session>
    {
        private ICollection<Speaker> speakers;

        public SessionFaker()
        {
            speakers = new SpeakerFaker().Generate(10);

            RuleFor(s => s.Title, f => f.Commerce.Random.Words());
            RuleFor(s => s.Description, f => f.Lorem.Paragraphs(1, 4));

            RuleFor(s => s.StartsAt, f => f.Date.SoonOffset());
            RuleFor(s => s.EndsAt, (f, s) =>
            {
                return s.StartsAt.AddMinutes(45);
            });

            RuleFor(s => s.Id, f =>
            {
                return f.Random.Number(999999).ToString();
            });

            RuleFor(s => s.Speakers, f =>
            {
                return f.PickRandom(speakers, f.Random.Number(1, 3)).ToArray();
            });

            RuleFor(s => s.Tags, f =>
            {
                return f.Random.WordsArray(f.Random.Number(0, 4));
            });

            RuleFor(s => s.Room, f => {
                return f.PickRandom(FakeRooms.Rooms);
            });

            RuleFor(s => s.SessionType, f => "Session");
        }

        public SessionFaker WithSpeakers(ICollection<Speaker> speakers)
        {
            this.speakers = speakers;
            return this;
        }
    }
}
