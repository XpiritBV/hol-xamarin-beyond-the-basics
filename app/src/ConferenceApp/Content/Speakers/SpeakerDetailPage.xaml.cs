using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ConferenceApp.Content.Speakers
{
    public partial class SpeakerDetailPage : ContentPage
    {
        public SpeakerDetailPage(SpeakerDetailViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}
