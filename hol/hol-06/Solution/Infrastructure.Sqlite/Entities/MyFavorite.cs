using System;
using SQLite;

namespace Infrastructure.Sqlite.Entities
{
    internal class MyFavorite
    {
        [PrimaryKey]
        public string SessionId { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
