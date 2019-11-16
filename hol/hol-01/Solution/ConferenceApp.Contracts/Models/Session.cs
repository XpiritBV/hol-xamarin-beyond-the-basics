using System;
using Newtonsoft.Json;

namespace ConferenceApp.Contracts.Models
{
    public class Session
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("startsAt")]
        public DateTimeOffset StartsAt { get; set; }

        [JsonProperty("endsAt")]
        public DateTimeOffset EndsAt { get; set; }

        [JsonProperty("isServiceSession")]
        public bool IsServiceSession { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("track")]
        public string Track { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("sessionType")]
        public string SessionType { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("speakers")]
        public Speaker[] Speakers { get; set; }

        [JsonIgnore]
        public bool IsFavorite { get; set; }
    }
}
