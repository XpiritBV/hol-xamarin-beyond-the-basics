using System;
using Bogus;
using ConferenceApp.Contracts.Models;

namespace Test.FakeData
{
    public class SpeakerFaker : Faker<Speaker>
    {
        public SpeakerFaker()
        {
            RuleFor(s => s.Id, f =>
            {
                return f.Random.Guid();
            });
            RuleFor(s => s.FirstName, f =>
            {
                return f.Person.FirstName;
            });
            RuleFor(s => s.LastName, f =>
            {
                return f.Person.LastName;
            });
            RuleFor(s => s.Bio, f =>
            {
                return f.Person.Random.Words();
            });
            RuleFor(s => s.TagLine, f =>
            {
                return f.Person.Random.Words(3);
            });
            RuleFor(s => s.CompanyWebsite, f =>
            {
                return new Uri("https://" + f.Person.Website);
            });
            RuleFor(s => s.ProfilePicture, f =>
            {
                return new Uri(f.Person.Avatar);
            });
            RuleFor(s => s.ProfilePictureSmall, f =>
            {
                return new Uri(f.Person.Avatar);
            });
            RuleFor(s => s.Blog, f =>
            {
                return new Uri("https://" + f.Person.Website);
            });
            RuleFor(s => s.Twitter, f =>
            {
                return new Uri("https://" + f.Person.Website);
            });
            RuleFor(s => s.LinkedIn, f =>
            {
                return new Uri("https://" + f.Person.Website);
            });
        }
    }
}
