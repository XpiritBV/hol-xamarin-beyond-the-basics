using Xamarin.Forms;

namespace ConferenceApp.Content.Sessions
{
    public partial class SessionDetailPage : ContentPage
    {
        public SessionDetailPage(string sessionId)
        {
            BindingContext = new SessionDetailViewModel(sessionId);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((SessionDetailViewModel)BindingContext).Activate();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((SessionDetailViewModel)BindingContext).Deactivate();
        }
    }
}
