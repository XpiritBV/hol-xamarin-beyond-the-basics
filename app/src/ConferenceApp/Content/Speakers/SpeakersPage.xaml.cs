using ConferenceApp.Contracts.Models;
using Xamarin.Forms;
using System.Linq;

namespace ConferenceApp.Content.Speakers
{
    public partial class SpeakersPage : ContentPage
    {
        public SpeakersPage()
        {
            InitializeComponent();
        }

        async void NavigateToSpeaker(object sender, SelectionChangedEventArgs e)
        {
            var speaker = e.CurrentSelection.FirstOrDefault() as Speaker;
            await Navigation.PushAsync(new SpeakerDetailPage(speaker.Id));
        }
    }
}