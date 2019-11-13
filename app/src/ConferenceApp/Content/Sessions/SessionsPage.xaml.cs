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

        async void NavigateToSession(object sender, SelectedItemChangedEventArgs e)
        {
            var session = e.SelectedItem as Session;
            await Navigation.PushAsync(new SessionDetailPage(session.Id));
        }
    }
}
