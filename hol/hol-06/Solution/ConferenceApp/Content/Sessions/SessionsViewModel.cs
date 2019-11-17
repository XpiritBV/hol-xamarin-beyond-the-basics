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
using MvvmHelpers;
using Shiny;
using Shiny.Notifications;
using Xamarin.Forms;

namespace ConferenceApp.Content.Sessions
{
    public class SessionsViewModel : BaseViewModel
    {
        private readonly IConferenceStore conferenceStore;
        private readonly ISyncService syncService;

        public SessionsViewModel()
        {
            Title = "Sessions";
            conferenceStore = ShinyHost.Resolve<IConferenceStore>();
            syncService = ShinyHost.Resolve<ISyncService>();

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
                await LoadSessionsInternal().ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task LoadSessionsInternal()
        {
            Sessions = await conferenceStore.GetSessions().ConfigureAwait(false);
            SessionsGrouped = sessions.GroupByStartTime();
        }

        private IEnumerable<Session> sessions = Enumerable.Empty<Session>();

        public IEnumerable<Session> Sessions
        {
            get { return sessions; }
            set { SetProperty(ref sessions, value); }
        }

        private IEnumerable<SessionGroup> sessionsGrouped = Enumerable.Empty<SessionGroup>();

        public IEnumerable<SessionGroup> SessionsGrouped
        {
            get { return sessionsGrouped; }
            set { SetProperty(ref sessionsGrouped, value); }
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
            try
            {
                if (SelectedSession == null)
                    return;

                var session = SelectedSession;
                SelectedSession = null;

                await Shell.Current.GoToAsync($"sessiondetail?sessionId={Uri.EscapeDataString(session.Id)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private IAsyncCommand syncCommand;
        public IAsyncCommand SyncCommand => syncCommand ?? (syncCommand = new AsyncCommand(SyncSessions));

        private async Task SyncSessions()
        {
            try
            {
                IsBusy = true;
                await syncService.SyncConferenceData(CancellationToken.None).ConfigureAwait(false);
                await LoadSessionsInternal().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during sync: {ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
