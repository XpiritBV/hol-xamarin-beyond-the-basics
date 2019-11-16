using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using ConferenceApp.Contracts;
using ConferenceApp.Contracts.Models;
using ConferenceApp.ViewModels;
using Shiny;

namespace ConferenceApp.Content.Speakers
{
    public class SpeakerDetailViewModel : BaseViewModel
    {
        readonly IConferenceStore conferenceStore;

        public SpeakerDetailViewModel(Guid speakerId)
        {
            conferenceStore = ShinyHost.Resolve<IConferenceStore>();

            SpeakerId = speakerId.ToString();
        }

        private async Task LoadSpeakerDetails()
        {
            if (Guid.TryParse(speakerId, out var id))
            {
                Speaker = await conferenceStore.GetSpeaker(id);
                Title = $"{Speaker.FirstName}'s profile";

                Sessions = await conferenceStore.GetSessionsForSpeaker(id);
            }
        }

        /// <summary>
        /// This property will be set upon navigation through the QueryProperty above
        /// </summary>
        private string speakerId;
        public string SpeakerId
        {
            get => speakerId;
            set
            {
                speakerId = value;
                LoadSpeakerDetails().SafeFireAndForget(onException: ex => Console.WriteLine($"Error while getting speaker details: {ex}"));
            }
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
