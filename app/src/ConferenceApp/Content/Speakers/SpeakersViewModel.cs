using System;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;
using MvvmHelpers;
using AsyncAwaitBestPractices;
using Xamarin.Forms;
using ConferenceApp.Services;

namespace ConferenceApp.Content.Speakers
{
    public class SpeakersViewModel : ViewModels.BaseViewModel
    {
        private readonly IConferenceStore conferenceStore;

        public SpeakersViewModel()
        {
            this.conferenceStore = DependencyService.Get<IConferenceStore>();
            LoadSpeakers().SafeFireAndForget(false, (ex) => Console.WriteLine(ex));
        }

        public async Task LoadSpeakers()
        {
            var speakers = await conferenceStore.GetSpeakers().ConfigureAwait(false);
            Speakers.ReplaceRange(speakers);
        }

        public ObservableRangeCollection<Speaker> Speakers { get; } = new ObservableRangeCollection<Speaker>();
    }
}
