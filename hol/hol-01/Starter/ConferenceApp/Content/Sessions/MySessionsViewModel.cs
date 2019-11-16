using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using ConferenceApp.Contracts;
using ConferenceApp.Contracts.Models;
using ConferenceApp.Services;
using ConferenceApp.ViewModels;
using Shiny;
using Xamarin.Forms;

namespace ConferenceApp.Content.Sessions
{
    public class MySessionsViewModel : BaseViewModel
    {
        private readonly IConferenceStore conferenceStore;
        private readonly ISyncService syncService;

        public MySessionsViewModel()
        {
            Title = "My Sessions";
            conferenceStore = ShinyHost.Resolve<IConferenceStore>();
            syncService = ShinyHost.Resolve<ISyncService>();
        }

        public override void Activate()
        {
            base.Activate();
            LoadSessions().SafeFireAndForget(false, (ex) => Console.WriteLine(ex));
        }

        private IAsyncCommand reloadSessionsCommand;
        public IAsyncCommand ReloadSessionsCommand => reloadSessionsCommand ?? (reloadSessionsCommand = new AsyncCommand(LoadSessions));

        public async Task LoadSessions()
        {
            IsBusy = true;

            try
            {
                await syncService.SyncIfNecessary(CancellationToken.None).ConfigureAwait(false);
                var sessions = await conferenceStore.GetSessions().ConfigureAwait(false);
                sessions = sessions.Where(s => s.IsFavorite);

                SessionsGrouped = sessions.GroupByStartTime();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private IEnumerable<SessionGroup> _sessionsGrouped = Enumerable.Empty<SessionGroup>();

        public IEnumerable<SessionGroup> SessionsGrouped
        {
            get { return _sessionsGrouped; }
            set { SetProperty(ref _sessionsGrouped, value); }
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

            await Shell.Current.GoToAsync($"sessiondetail?sessionId={Uri.EscapeDataString(session.Id)}");
        }
    }
}
