using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using ConferenceApp.Contracts;
using ConferenceApp.Contracts.Models;
using ConferenceApp.ViewModels;
using Shiny;
using Xamarin.Forms;

namespace ConferenceApp.Content.Sessions
{
    [QueryProperty(nameof(SessionId), "sessionId")]
    public class SessionDetailViewModel : BaseViewModel
    {
        private readonly IConferenceStore conferenceStore;

        public SessionDetailViewModel()
        {
            conferenceStore = ShinyHost.Resolve<IConferenceStore>();
        }

        /// <summary>
        /// This property will be set upon navigation through the QueryProperty above
        /// </summary>
        private string sessionId;
        public string SessionId
        {
            get { return sessionId; }
            set
            {
                sessionId = value;

                GetSelectedSession().SafeFireAndForget(onException: ex => Console.WriteLine(ex));
            }
        }

        private async Task GetSelectedSession()
        {
            if (string.IsNullOrEmpty(SessionId))
                return;

            Session = await conferenceStore.GetSession(SessionId);
        }

        private Session session;
        public Session Session
        {
            get { return session; }
            set
            {
                SetProperty(ref session, value);
                OnPropertyChanged(nameof(FormattedSessionTime));
                OnPropertyChanged(nameof(IsFavorite));
            }
        }

        public string FormattedSessionTime =>
            session?.StartsAt.ToLocalTime().ToString("t") + " - " + session?.EndsAt.ToLocalTime().ToString("t");

        private IAsyncCommand toggleFavoriteCommand;
        public IAsyncCommand ToggleFavoriteCommand => toggleFavoriteCommand ?? (toggleFavoriteCommand = new AsyncCommand(ToggleFavorite));

        public bool IsFavorite
        {
            get => Session?.IsFavorite ?? false;
            set
            {
                Session.IsFavorite = value;
                OnPropertyChanged(nameof(IsFavorite));
            }
        }

        private async Task ToggleFavorite()
        {
            IsBusy = true;

            try
            {
                if (IsFavorite)
                {
                    await conferenceStore.UnfavoriteSession(Session.Id).ConfigureAwait(false);
                }
                else
                {
                    await conferenceStore.FavoriteSession(Session.Id).ConfigureAwait(false);
                }

                IsFavorite = !IsFavorite;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
