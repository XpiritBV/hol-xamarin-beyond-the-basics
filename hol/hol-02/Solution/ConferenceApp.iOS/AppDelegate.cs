using ConferenceApp.Contracts;
using ConferenceApp.iOS.Services;
using ConferenceApp.Services;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using UIKit;

namespace ConferenceApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Shiny.iOSShinyHost.Init(new Startup(), builder =>
            {
                //TODO: register iOS specific dependencies here
                builder.AddTransient<ISetReminder, SetReminderImpl>();
            });

            global::Xamarin.Forms.Forms.Init();
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            global::Xamarin.Forms.FormsMaterial.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
