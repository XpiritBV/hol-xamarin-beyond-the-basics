using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace ConferenceWeb.Data
{
    public static class DataStore
    {
        static DataStore()
        {
            var json = GetEmbeddedResourceString(Assembly.GetExecutingAssembly(), "ConferenceWeb.Data.LocalConferenceStore.json");

            Sessions = JsonConvert.DeserializeObject<IEnumerable<Session>>(json);

            Speakers = Sessions.SelectMany(s => s.Speakers)
                .Distinct(new LocalSpeakerComparer())
                .OrderBy(s => s.FirstName)
                .ThenBy(s => s.LastName)
                .ToList();
        }

        public static IEnumerable<Session> Sessions { get; private set; }
        public static IEnumerable<Speaker> Speakers { get; private set; }

        private class LocalSpeakerComparer : IEqualityComparer<Speaker>
        {
            public bool Equals(Speaker x, Speaker y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Speaker obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        public static string GetEmbeddedResourceString(Assembly assembly, string filename)
        {
            var resourceNames = assembly.GetManifestResourceNames();
            var fqFileName = resourceNames.Where(name => name.Contains(filename)).FirstOrDefault();
            if (string.IsNullOrEmpty(fqFileName))
                throw new ArgumentException($"file {filename} not found as embeded resource.");

            Stream stream = assembly.GetManifestResourceStream(fqFileName);
            string text = "";
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
    }
}
