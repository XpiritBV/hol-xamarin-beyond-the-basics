using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ConferenceApp.Content.Sessions
{
    public partial class SessionDetailPage : ContentPage
    {
        public SessionDetailPage(SessionDetailViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}
