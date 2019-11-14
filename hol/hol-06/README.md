# Exercise 3 - Resilient Connected Apps

In this module, we will spice up the app a little bit with deeper OS integrations like AppLinks, haptic feedback and a widget.

## Goals for this exercise

- [Add haptic feedback to the Favorite button](#1)
- [Implement deep links and content indexing with AppLinks](#2)
- [Implement an Android Widget to complement the app](#3)

## <a nane="1"></a> Haptic feedback

Apart from only providing visual feedback, an app could also provide haptic feedback to a user when something happens or when the user performs an action. On Android, we can use the _vibrate_ function of the device, and newer iPhones (7 and up) even have a _taptic engine_, which can provide very precise haptic feedback.

Let's tap into these features.

1. In the `ConferenceApp.Contracts` project, add an interface named `IHapticFeedback`:

    ```csharp
    using System;

    namespace ConferenceApp.Contracts
    {
        public interface IHapticFeedback
        {
            void Success();
            void Error();
        }
    }
    ```

    >This interface allows us to provide two types of feedback: success or error

2. On Android, we need to give the app permission to trigger the vibration. Make sure that the `Vibrate` permission is added to the Android manifest:

    ```xml
    <uses-permission android:name="android.permission.VIBRATE" />
    ```

3. In `Services` folder of the `ConferenceApp.Android` project, add a new class named `HapticFeedback` and implement the `IHapticFeedback` interface we just created:

    ```csharp
    using System;
    using ConferenceApp.Contracts;

    namespace ConferenceApp.Droid.Services
    {
        public class HapticFeedback : IHapticFeedback
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
    ```

    >For Android, we use the `Xamarin.Essentials.Vibration` class, which is cross platform, so it could be used in iOS as well. But for iOS, we want to leverage the _taptic engine_.

4. Also make sure that the new `HapticFeedback` class is registered in the dependency injection container. Since this is a platform specific service, we can only register it in the `MainApplication`'s `OnCreate` method:

    ```csharp
    builder.AddTransient<IHapticFeedback, HapticFeedback>();
    ```

5. In the `Services` folder of the `ConferenceApp.iOS` project, add a new class named `HapticFeedback` and implement the `IHapticFeedback` interface:

    ```csharp
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
    ```

    >This class uses the iOS native `UINotificationFeedbackGenerator` API. Notice that it calls `.Prepare()` in the contstructor. This is to notify the OS that it is going to invoke the taptic engine soon. Doing this will trigger the engine with lower latency.
    
    >Also note that for iOS, the `UINotificationFeedbackGenerator` needs to be invoked from the UI thread, hence the `Device.BeginInvokeOnMainThread`.

6. Also make sure that the new `HapticFeedback` class is registered in the dependency injection container. Since this is a platform specific service, we can only register it in the `AppDelegate`'s `FinishedLaunching` method:

    ```csharp
    builder.AddTransient<IHapticFeedback, HapticFeedback>();
    ```

Now we are ready to consume the `IHapticFeedback` service from the app.

7. In `SessionDetailViewModel`, add a private field to the class that will hold the reference to the `IHapticFeedback` service:

    ```csharp
    private readonly IHapticFeedback hapticFeedback;
    ```

8. Resolve the interface in the class constructor:

    ```csharp
    hapticFeedback = ShinyHost.Resolve<IHapticFeedback>();
    ```

7. In `SessionDetailViewModel`, find the `IsFavorite` property setter and add a call to the `hapticFeedback` service:

    ```csharp
    hapticFeedback.Success();
    ```

    >This can only be tested on a physical device