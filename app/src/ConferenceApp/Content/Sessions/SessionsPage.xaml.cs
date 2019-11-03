using System;
using AsyncAwaitBestPractices;
using Xamarin.Forms;

namespace ConferenceApp.Content.Sessions
{
    public partial class SessionsPage : ContentPage
    {
        public SessionsPage()
        {
            BindingContext = new SessionsViewModel();
            InitializeComponent();
        }
    }
}
