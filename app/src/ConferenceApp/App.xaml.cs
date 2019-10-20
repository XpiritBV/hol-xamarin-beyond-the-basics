using Xamarin.Forms;
using ConferenceApp.Content.Sessions;
using ConferenceApp.Content.Speakers;

namespace ConferenceApp
{
    public partial class App : Application
    {
        public static string AppTheme { get; set; }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ISessionStore, SessionStore>();
            DependencyService.Register<ISpeakerStore, SpeakerStore>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
