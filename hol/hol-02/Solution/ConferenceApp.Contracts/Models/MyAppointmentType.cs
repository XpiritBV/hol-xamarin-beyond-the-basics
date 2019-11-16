using System;
namespace ConferenceApp.Contracts.Models
{
    public class MyAppointmentType
    {
        public string Title { get; set; }
        public string WhereWhen { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Description { get; set; }
    }
}
