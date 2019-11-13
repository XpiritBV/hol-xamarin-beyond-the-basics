using System.Linq;
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

        async void NavigateToSession(object sender, SelectionChangedEventArgs e)
        {
            var session = e.CurrentSelection.FirstOrDefault() as Session;
            await Navigation.PushAsync(new SessionDetailPage(session.Id));
        }
    }
}
