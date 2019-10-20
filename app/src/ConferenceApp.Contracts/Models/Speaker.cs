using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ConferenceApp.Contracts.Models
{
    public partial class Speaker
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("tagLine")]
        public string TagLine { get; set; }

        [JsonProperty("profilePictureSmall")]
        public Uri ProfilePictureSmall { get; set; }

        [JsonProperty("profilePicture")]
        public Uri ProfilePicture { get; set; }

        [JsonProperty("twitter")]
        public Uri Twitter { get; set; }

        [JsonProperty("linkedIn")]
        public Uri LinkedIn { get; set; }

        [JsonProperty("blog")]
        public Uri Blog { get; set; }

        [JsonProperty("companyWebsite")]
        public Uri CompanyWebsite { get; set; }
    }
}