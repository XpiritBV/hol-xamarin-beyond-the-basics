using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
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
            this.sessionStore = DependencyService.Get<ISessionStore>();
            LoadSessions().SafeFireAndForget(false, (ex) => Console.WriteLine(ex));
        }

        public async Task LoadSessions()
        {
            var sessions = await sessionStore.GetSessions().ConfigureAwait(false);
            Sessions.ReplaceRange(sessions);
        }

        public ObservableRangeCollection<Session> Sessions { get; } = new ObservableRangeCollection<Session>();
    }
}
