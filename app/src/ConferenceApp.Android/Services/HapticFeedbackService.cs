using System;
using ConferenceApp.Contracts;

namespace ConferenceApp.Droid.Services
{
    public class HapticFeedbackService : IHapticFeedback
    {
        public void Error()
        {
            Xamarin.Essentials.Vibration.Vibrate(100);
        }

        public void Success()
        {
            Xamarin.Essentials.Vibration.Vibrate(10);
        }
    }
}
