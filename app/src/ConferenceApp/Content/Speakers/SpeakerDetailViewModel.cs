using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using ConferenceApp.Contracts.Models;
using ConferenceApp.Services;
using ConferenceApp.ViewModels;
using Xamarin.Forms;

namespace ConferenceApp.Content.Speakers
{
    public class SpeakerDetailViewModel : BaseViewModel
    {
        IConferenceStore conferenceStore;

        public SpeakerDetailViewModel()
        {
            // only for designer
            conferenceStore = DependencyService.Get<IConferenceStore>();
        }

        public SpeakerDetailViewModel(Speaker item) : this()
        {
            Speaker = item;
            Title = $"{Speaker.FirstName} {Speaker.LastName}";

            LoadSessions().SafeFireAndForget();
        }

        private async Task LoadSessions()
        {
            Sessions = await conferenceStore.GetSessionsForSpeaker(speaker.Id);
        }

        private Speaker speaker;
        public Speaker Speaker
        {
            get { return speaker; }
            set { SetProperty(ref speaker, value); }
        }

        private IEnumerable<Session> sessions;
        public IEnumerable<Session> Sessions
        {
            get { return sessions; }
            set { SetProperty(ref sessions, value); }
        }
    }
}
