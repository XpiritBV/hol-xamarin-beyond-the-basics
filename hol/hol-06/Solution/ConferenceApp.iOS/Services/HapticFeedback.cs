using ConferenceApp.Contracts;
using UIKit;
using Xamarin.Forms;

namespace ConferenceApp.iOS.Services
{
    public class HapticFeedback : IHapticFeedback
    {
        UINotificationFeedbackGenerator _feedback;

        public HapticFeedback()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // The UINotificationFeedbackGenerator will use the Taptic Engine in newer iPhones
                _feedback = new UINotificationFeedbackGenerator();
                _feedback.Prepare();
            }
        }

        public void Success()
        {
            Device.BeginInvokeOnMainThread(() => {
                _feedback?.NotificationOccurred(UINotificationFeedbackType.Success);
            });
        }

        public void Error()
        {
            Device.BeginInvokeOnMainThread(() => {
                _feedback?.NotificationOccurred(UINotificationFeedbackType.Error);
            });
        }
    }
}
