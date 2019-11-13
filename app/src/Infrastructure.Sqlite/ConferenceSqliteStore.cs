using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConferenceApp.Contracts;
using Infrastructure.Sqlite.Entities;
using SQLite;

namespace Infrastructure.Sqlite
{
    public class ConferenceSqliteStore: IConferenceStore
    {
        private static readonly SQLiteAsyncConnection connection;
        private static bool hasData;

        static ConferenceSqliteStore()
        {
            SQLitePCL.Batteries_V2.Init();

            connection = GetAsyncConnection();

            // setting up the database tables must be done synchronously at startup
            var syncConnection = connection.GetConnection();
            syncConnection.CreateTable<Session>();
            syncConnection.CreateTable<Speaker>();
            syncConnection.CreateTable<MyFavorite>();

            hasData = syncConnection.Table<Session>().Any();
        }

        private static SQLiteAsyncConnection GetAsyncConnection()
        {
            var databaseLocation = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "conference.db");
            var asyncConnection = new SQLiteAsyncConnection(databaseLocation, true);
            //asyncConnection.GetConnection().Tracer = line => Debug.WriteLine(line);
            //asyncConnection.GetConnection().TimeExecution = false;
            //asyncConnection.GetConnection().Trace = true;
            return asyncConnection;
        }

        public bool HasData() => hasData;

        public async Task ReplaceData(IEnumerable<ConferenceApp.Contracts.Models.Session> sessions)
        {
            if (sessions == null || !sessions.Any())
            {
                return; // nothing to store
            }

            await connection.RunInTransactionAsync((SQLiteConnection tran) =>
            {
                tran.DeleteAll<Speaker>();
                tran.DeleteAll<Session>();

                var sessionEntities = sessions
                    .Select(s => s.ToEntity())
                    .ToList();

                var speakerEntities = sessions
                    .SelectMany(s => s.Speakers)
                    .Distinct(new SpeakerComparer())
                    .Select(s => s.ToEntity())
                    .ToList();

                tran.InsertAll(speakerEntities, typeof(Speaker));
                tran.InsertAll(sessionEntities, typeof(Session));

                // Delete favorites for sessions that don't exist anymore
                tran.Execute("DELETE FROM MyFavorite WHERE NOT EXISTS (SELECT Id FROM Session WHERE Id = SessionId)");

                hasData = tran.Table<Session>().Any();
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ConferenceApp.Contracts.Models.Session>> GetSessions()
        {
            var sessions = await connection.Table<Session>()
                .OrderBy(s => s.StartsAt)
                .ToArrayAsync()
                .ConfigureAwait(false);

            if (sessions != null)
            {
                var allSpeakers = await connection.Table<Speaker>()
                    .OrderBy(s => s.FirstName)
                    .ThenBy(s => s.LastName)
                    .ToArrayAsync()
                    .ConfigureAwait(false);

                var allFavorites = await connection.Table<MyFavorite>()
                    .ToArrayAsync()
                    .ConfigureAwait(false);

                return sessions.Select(s => s.ToModel(allSpeakers, allFavorites)).ToList();
            }

            return null;
        }

        public async Task<ConferenceApp.Contracts.Models.Session> GetSession(string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            var session = await connection.Table<Session>()
                .FirstOrDefaultAsync(s => s.Id == sessionId)
                .ConfigureAwait(false);

            if (session != null)
            {
                var speakers = await GetSpeakersForSession(sessionId).ConfigureAwait(false);

                var favorite = await connection.Table<MyFavorite>()
                    .Where(f => f.SessionId == sessionId)
                    .ToArrayAsync()
                    .ConfigureAwait(false);

                return session.ToModel(speakers.ToArray(), favorite);
            }

            return null;
        }

        public async Task<ConferenceApp.Contracts.Models.Speaker> GetSpeaker(Guid speakerId)
        {
            var filter = speakerId.ToString();

            var speaker = await connection.Table<Speaker>()
                .FirstOrDefaultAsync(s => s.Id == filter)
                .ConfigureAwait(false);

            if (speaker != null)
            {
                return speaker.ToModel();
            }

            return null;
        }

        public async Task<IEnumerable<ConferenceApp.Contracts.Models.Session>> GetSessionsForSpeaker(Guid speakerId)
        {
            var filter = speakerId.ToString();

            var sessions = await connection.Table<Session>()
                .Where(s => s.SpeakerIds.Contains(filter))
                .OrderBy(s => s.StartsAt)
                .ToArrayAsync()
                .ConfigureAwait(false);

            var allFavorites = await connection.Table<MyFavorite>()
                .ToArrayAsync()
                .ConfigureAwait(false);

            return sessions.Select(s => s.ToModel(favorites: allFavorites));
        }

        private async Task<IEnumerable<Speaker>> GetSpeakersForSession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            var session = await connection.Table<Session>()
                .FirstOrDefaultAsync(s => s.Id == sessionId)
                .ConfigureAwait(false);

            if (session != null)
            {
                var speakers = await connection.Table<Speaker>()
                    .Where(s => session.SpeakerIds.Contains(s.Id))
                    .OrderBy(s => s.FirstName)
                    .ThenBy(s => s.LastName)
                    .ToArrayAsync()
                    .ConfigureAwait(false);

                return speakers;

            }

            return null;
        }

        public async Task<IEnumerable<ConferenceApp.Contracts.Models.Speaker>> GetSpeakers()
        {
            var speakers = await connection
                .Table<Speaker>()
                .OrderBy(s => s.FirstName)
                .ThenBy(s => s.LastName)
                .ToListAsync()
                .ConfigureAwait(false);

            if (speakers != null)
            {
                return speakers
                    .Select(s => s.ToModel())
                    .ToList();
            }

            return null;
        }

        public Task FavoriteSession(string sessionId)
        {
            var favorite = new MyFavorite { SessionId = sessionId, Created = DateTimeOffset.UtcNow };

            return connection.InsertOrReplaceAsync(favorite, typeof(MyFavorite));
        }

        public Task UnfavoriteSession(string sessionId)
        {
            return connection
                .Table<MyFavorite>()
                .DeleteAsync(f => f.SessionId == sessionId);
        }

        private class SpeakerComparer : IEqualityComparer<ConferenceApp.Contracts.Models.Speaker>
        {
            public bool Equals(ConferenceApp.Contracts.Models.Speaker x, ConferenceApp.Contracts.Models.Speaker y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(ConferenceApp.Contracts.Models.Speaker obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
