using Shiny.Locations;
using Shiny.Notifications;
using System.Threading.Tasks;

namespace ConferenceApp.Services
{
    public class LocationDelegate : IGeofenceDelegate
    {
        readonly INotificationManager notifications;

        public LocationDelegate(INotificationManager notifications)
        {
            this.notifications = notifications;
        }


        public async Task OnStatusChanged(GeofenceState newStatus, GeofenceRegion region)
        {
            if (newStatus == GeofenceState.Entered)
            {
                await this.notifications.Send(new Notification
                {
                    Title = "WELCOME!",
                    Message = "you entered the geofence region " + region.Identifier
                });
            }
            else
            {
                await this.notifications.Send(new Notification
                {
                    Title = "GOODBYE!",
                    Message = "You exited the geofence region " + region.Identifier
                });
            }
        }
    }
}