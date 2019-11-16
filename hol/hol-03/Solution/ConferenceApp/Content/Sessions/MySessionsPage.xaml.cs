
using Xamarin.Forms;

namespace ConferenceApp.Content.Sessions
{
    public partial class MySessionsPage : ContentPage
    {
        public MySessionsPage()
        {
            BindingContext = new SessionsViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((MySessionsViewModel)BindingContext).Activate();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((MySessionsViewModel)BindingContext).Deactivate();
        }
    }
}
