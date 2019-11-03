using ConferenceApp.Contracts.Models;
using ConferenceApp.Services;
using ConferenceApp.ViewModels;
using Humanizer;

namespace ConferenceApp.Content.Sessions
{
    public class SessionDetailViewModel : BaseViewModel
    {
        public SessionDetailViewModel()
        {
            // only for designer
        }

        public SessionDetailViewModel(Session session)
        {
            Title = session.Title;
            Session = session;
        }

        private Session session;
        public Session Session
        {
            get { return session; }
            set { SetProperty(ref session, value); }
        }

        public string FormattedSessionTime =>
            session.StartsAt.ToLocalTime().ToString("t") + " - " + session.EndsAt.ToLocalTime().ToString("t");
    }
}
