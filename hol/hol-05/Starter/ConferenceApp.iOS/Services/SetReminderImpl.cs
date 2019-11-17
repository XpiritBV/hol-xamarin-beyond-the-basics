using System;
using System.Threading.Tasks;
using ConferenceApp.Contracts;
using ConferenceApp.Contracts.Models;
using EventKit;
using Foundation;

namespace ConferenceApp.iOS.Services
{
    public class SetReminderImpl : ISetReminder
    {
        public async Task<bool> AddAppointment(MyAppointmentType appointment)
        {
            var eventStore = new EKEventStore();
            var granted = await eventStore.RequestAccessAsync(EKEntityType.Event);
            if (granted.Item1)
            {
                EKEvent newEvent = EKEvent.FromStore(eventStore);
                newEvent.StartDate = DateTimeToNSDate(appointment.ExpireDate);
                newEvent.EndDate = DateTimeToNSDate(appointment.ExpireDate.AddHours(1));
                newEvent.Title = appointment.Title; newEvent.Notes = appointment.WhereWhen;
                newEvent.Calendar = eventStore.DefaultCalendarForNewEvents;
                return eventStore.SaveEvent(newEvent, EKSpan.ThisEvent, out NSError e);
            }
            return false;
        }

        public NSDate DateTimeToNSDate(DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);
            }
            return (NSDate)date;
        }
    }
}
