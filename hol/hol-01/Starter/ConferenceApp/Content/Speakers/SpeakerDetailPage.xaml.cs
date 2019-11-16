using System;
using Xamarin.Forms;

namespace ConferenceApp.Content.Speakers
{
    public partial class SpeakerDetailPage : ContentPage
    {
        public SpeakerDetailPage(Guid speakerId)
        {
            BindingContext = new SpeakerDetailViewModel(speakerId);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((SpeakerDetailViewModel)BindingContext).Activate();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((SpeakerDetailViewModel)BindingContext).Deactivate();
        }
    }
}
