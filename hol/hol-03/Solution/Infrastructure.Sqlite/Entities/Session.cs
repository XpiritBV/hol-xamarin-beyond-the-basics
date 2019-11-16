using System;
using SQLite;

namespace Infrastructure.Sqlite.Entities
{
    internal class Session
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartsAt { get; set; }
        public DateTimeOffset EndsAt { get; set; }
        public bool IsServiceSession { get; set; }
        public string Room { get; set; }
        public string Track { get; set; }
        public string Level { get; set; }
        public string SessionType { get; set; }
        public string Tags { get; set; }
        public string SpeakerIds { get; set; }
    }
}
