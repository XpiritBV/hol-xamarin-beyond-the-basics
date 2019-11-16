using System;
using SQLite;

namespace Infrastructure.Sqlite.Entities
{
    internal class Speaker
    {
        [PrimaryKey]
        public string Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Bio { get; set; }

        public string TagLine { get; set; }

        public string ProfilePictureSmall { get; set; }

        public string ProfilePicture { get; set; }

        public string Twitter { get; set; }

        public string LinkedIn { get; set; }

        public string Blog { get; set; }

        public string CompanyWebsite { get; set; }
    }
}
