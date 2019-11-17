using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using ConferenceApp.Services;
using ConferenceApp.Styles;
using Firebase;
using Shiny;
using Xamarin.Forms.Platform.Android.AppLinks;

namespace ConferenceApp.Droid
{
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                   Categories = new[]
                   {
                            Android.Content.Intent.CategoryDefault,
                            Android.Content.Intent.CategoryBrowsable
                   },
                   DataScheme = "http", DataHost = APPLINK_DATAHOST, DataPathPrefix = "/sessions/", AutoVerify = true)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                   Categories = new[]
                   {
                            Android.Content.Intent.CategoryDefault,
                            Android.Content.Intent.CategoryBrowsable
                   },
                   DataScheme = "https", DataHost = APPLINK_DATAHOST, DataPathPrefix = "/sessions/", AutoVerify = true)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                   Categories = new[]
                   {
                            Android.Content.Intent.CategoryDefault,
                            Android.Content.Intent.CategoryBrowsable
                   },
                   DataScheme = "http", DataHost = APPLINK_DATAHOST, DataPathPrefix = "/speakers/", AutoVerify = true)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                   Categories = new[]
                   {
                            Android.Content.Intent.CategoryDefault,
                            Android.Content.Intent.CategoryBrowsable
                   },
                   DataScheme = "https", DataHost = APPLINK_DATAHOST, DataPathPrefix = "/speakers/", AutoVerify = true)]
    [Activity(Label = "ConferenceApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const string APPLINK_DATAHOST = "conferenceapp-demo.azurewebsites.net";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);

            FirebaseApp.InitializeApp(this);
            AndroidAppLinks.Init(this);

            LoadApplication(new App());

            ThemeService.ApplyTheme(GetOSTheme(Resources.Configuration));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            AndroidShinyHost.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            ThemeService.ApplyTheme(GetOSTheme(newConfig));
        }

        public string GetOSTheme(Configuration configuration)
        {
            //Ensure the device is running Android Froyo or higher because UIMode was added in Android Froyo, API 8.0
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Froyo)
            {
                var uiModeFlags = configuration.UiMode & UiMode.NightMask;

                switch (uiModeFlags)
                {
                    case UiMode.NightYes:
                        return nameof(DarkTheme);
                    case UiMode.NightNo:
                        return nameof(LightTheme);
                }
            }

            // default theme
            return nameof(LightTheme);
        }
    }
}