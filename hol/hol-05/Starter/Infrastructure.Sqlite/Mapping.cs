using System;
using System.Collections.Generic;
using System.Linq;
using ConferenceApp.Contracts.Models;

namespace Infrastructure.Sqlite
{
    internal static class Mapping
    {
        public static Entities.Session ToEntity(this Session session)
        {
            return new Entities.Session
            {
                Id = session.Id,
                Description = session.Description,
                EndsAt = session.EndsAt,
                IsServiceSession = session.IsServiceSession,
                Level = session.Level,
                Room = session.Room,
                SessionType = session.SessionType,
                StartsAt = session.StartsAt,
                Tags = string.Join("||", session.Tags),
                Title = session.Title,
                Track = session.Track,
                SpeakerIds = string.Join(';', session.Speakers.Select(s => s.Id.ToString()))
            };
        }

        public static Session ToModel(this Entities.Session session, IEnumerable<Entities.Speaker> speakers = null, IEnumerable<Entities.MyFavorite> favorites = null)
        {
            Entities.Speaker[] sessionSpeakers = Array.Empty<Entities.Speaker>();

            if (speakers != null)
            {
                sessionSpeakers = speakers
                    .Where(s => session.SpeakerIds.Contains(s.Id.ToString()))
                    .OrderBy(s => s.FirstName)
                    .ThenBy(s => s.LastName)
                    .ToArray();
            }

            return new Session
            {
                Id = session.Id,
                Description = session.Description,
                EndsAt = session.EndsAt,
                IsServiceSession = session.IsServiceSession,
                Level = session.Level,
                Room = session.Room,
                SessionType = session.SessionType,
                Speakers = sessionSpeakers.Select(s => s.ToModel()).ToArray(),
                StartsAt = session.StartsAt,
                Tags = !string.IsNullOrWhiteSpace(session.Tags) ? session.Tags.Split("||") : null,
                Title = session.Title,
                Track = session.Track,
                IsFavorite = favorites?.Any(f => f.SessionId == session.Id) ?? false
            };
        }

        public static Entities.Speaker ToEntity(this Speaker speaker)
        {
            return new Entities.Speaker
            {
                Id = speaker.Id.ToString(),
                Bio = speaker.Bio,
                Blog = speaker.Blog?.AbsoluteUri,
                CompanyWebsite = speaker.CompanyWebsite?.AbsoluteUri,
                FirstName = speaker.FirstName,
                LastName = speaker.LastName,
                LinkedIn = speaker.CompanyWebsite?.AbsoluteUri,
                ProfilePicture = speaker.ProfilePicture?.AbsoluteUri,
                ProfilePictureSmall = speaker.ProfilePictureSmall?.AbsoluteUri,
                TagLine = speaker.TagLine,
                Twitter = speaker.Twitter?.AbsoluteUri
            };
        }

        public static Speaker ToModel(this Entities.Speaker speaker)
        {
            return new Speaker
            {
                Id = Guid.Parse(speaker.Id),
                Bio = speaker.Bio,
                Blog = speaker.Blog.ToUri(),
                CompanyWebsite = speaker.CompanyWebsite.ToUri(),
                FirstName = speaker.FirstName,
                LastName = speaker.LastName,
                LinkedIn = speaker.CompanyWebsite.ToUri(),
                ProfilePicture = speaker.ProfilePicture.ToUri(),
                ProfilePictureSmall = speaker.ProfilePictureSmall.ToUri(),
                TagLine = speaker.TagLine,
                Twitter = speaker.Twitter.ToUri()
            };
        }

        private static Uri ToUri(this string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                return new Uri(url);
            }

            return null;
        }
    }
}
