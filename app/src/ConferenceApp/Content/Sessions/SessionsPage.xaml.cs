using System;
using System.Linq;
using AsyncAwaitBestPractices;
using ConferenceApp.Contracts.Models;
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
