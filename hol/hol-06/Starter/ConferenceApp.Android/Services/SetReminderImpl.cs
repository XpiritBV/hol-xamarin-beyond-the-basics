using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Provider;
using ConferenceApp.Contracts;
using ConferenceApp.Contracts.Models;
using Java.Util;
using Plugin.CurrentActivity;

namespace ConferenceApp.Droid.Services
{
    public class SetReminderImpl : ISetReminder
    {
        public Task<bool> AddAppointment(MyAppointmentType appointment)
        {
            var intent = new Intent(Intent.ActionInsert);
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Title, appointment.Title);
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Description, appointment.WhereWhen + " " + appointment.Description);
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(appointment.ExpireDate));
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(appointment.ExpireDate.AddHours(1)));
            intent.PutExtra(CalendarContract.ExtraEventBeginTime, GetDateTimeMS(appointment.ExpireDate));
            intent.PutExtra(CalendarContract.ExtraEventEndTime, GetDateTimeMS(appointment.ExpireDate.AddHours(1)));
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
            intent.SetData(CalendarContract.Events.ContentUri);
            CrossCurrentActivity.Current.Activity.StartActivity(intent);

            return Task.FromResult(true);
        }

        long GetDateTimeMS(DateTime time)
        {
            var c = Calendar.GetInstance(Java.Util.TimeZone.Default);

            c.Set(Java.Util.CalendarField.DayOfMonth, time.Day);
            c.Set(Java.Util.CalendarField.HourOfDay, time.Hour);
            c.Set(Java.Util.CalendarField.Minute, time.Minute);
            c.Set(Java.Util.CalendarField.Month, time.Month);
            c.Set(Java.Util.CalendarField.Year, time.Year);

            return c.TimeInMillis;
        }
    }
}
