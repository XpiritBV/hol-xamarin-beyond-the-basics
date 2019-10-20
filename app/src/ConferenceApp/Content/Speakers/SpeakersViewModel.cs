using System;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;
using MvvmHelpers;
using AsyncAwaitBestPractices;
using Xamarin.Forms;

namespace ConferenceApp.Content.Speakers
{
    public class SpeakersViewModel : ViewModels.BaseViewModel
    {
        private readonly ISpeakerStore speakerStore;

        public SpeakersViewModel()
        {
            this.speakerStore = DependencyService.Get<ISpeakerStore>();
            LoadSpeakers().SafeFireAndForget(false, (ex) => Console.WriteLine(ex));
        }

        public async Task LoadSpeakers()
        {
            var speakers = await speakerStore.GetSpeakers().ConfigureAwait(false);
            Speakers.ReplaceRange(speakers);
        }

        public ObservableRangeCollection<Speaker> Speakers { get; } = new ObservableRangeCollection<Speaker>();
    }
}
