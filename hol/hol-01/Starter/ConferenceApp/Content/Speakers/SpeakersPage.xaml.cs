using ConferenceApp.Contracts.Models;
using Xamarin.Forms;

namespace ConferenceApp.Content.Speakers
{
    public partial class SpeakersPage : ContentPage
    {
        public SpeakersPage()
        {
            InitializeComponent();
        }

        async void NavigateToSpeaker(object sender, SelectedItemChangedEventArgs e)
        {
            var speaker = e.SelectedItem as Speaker;
            await Navigation.PushAsync(new SpeakerDetailPage(speaker.Id));
        }
    }
}