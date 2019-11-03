using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using ConferenceApp.Contracts.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace ConferenceApp.Content.Sessions
{
    public class SessionsViewModel : BaseViewModel
    {
        private readonly ISessionStore sessionStore;

        public SessionsViewModel()
        {
            Title = "Sessions";
            this.sessionStore = DependencyService.Get<ISessionStore>();
            LoadSessions().SafeFireAndForget(false, (ex) => Console.WriteLine(ex));
        }

        private IAsyncCommand reloadSessionsCommand;
        public IAsyncCommand ReloadSessionsCommand => reloadSessionsCommand ?? (reloadSessionsCommand = new AsyncCommand(LoadSessions));

        public async Task LoadSessions()
        {
            IsBusy = true;
            var sessions = await sessionStore.GetSessions().ConfigureAwait(false);

            SessionsGrouped = sessions.FilterAndGroupByDate();

            IsBusy = false;
        }

        private IList<SessionGroup> _sessionsGrouped;

        public IList<SessionGroup> SessionsGrouped
        {
            get { return _sessionsGrouped; }
            set { SetProperty(ref _sessionsGrouped, value); }
        }

        private IList<Session> _sessions;
        public IList<Session> Sessions
        {
            get { return _sessions; }
            set { SetProperty(ref _sessions, value); }
        }

        private Session selectedSession;
        public Session SelectedSession
        {
            get { return selectedSession; }
            set
            {
                SetProperty(ref selectedSession, value);
            }
        }

        private IAsyncCommand selectSessionCommand;
        public IAsyncCommand SelectSessionCommand => selectSessionCommand ?? (selectSessionCommand = new AsyncCommand(NavigateToSession));

        private async Task NavigateToSession()
        {
            if (SelectedSession == null)
                return;

            var session = SelectedSession;
            SelectedSession = null;

            await Shell.Current.Navigation.PushAsync(new SessionDetailPage(new SessionDetailViewModel(session)));
        }
    }
}