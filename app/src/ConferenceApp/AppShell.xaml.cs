using System;
using System.Collections.Generic;
using ConferenceApp.Content.Sessions;
using ConferenceApp.Content.Speakers;
using Xamarin.Forms;

namespace ConferenceApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        void RegisterRoutes()
        {
            Routing.RegisterRoute("sessions", typeof(SessionsPage));
            Routing.RegisterRoute("mysessions", typeof(MySessionsPage));
            Routing.RegisterRoute("sessiondetail", typeof(SessionDetailPage));
            Routing.RegisterRoute("speakers", typeof(SpeakersPage));
            Routing.RegisterRoute("speakerdetail", typeof(SpeakerDetailPage));
        }
    }
}
