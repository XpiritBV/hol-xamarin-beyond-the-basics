using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using ConferenceApp.Contracts.Models;
using ConferenceApp.Services;
using MvvmHelpers;
using Xamarin.Forms;

namespace ConferenceApp.Content.Speakers
{
    public class SpeakersViewModel : ViewModels.BaseViewModel
    {
        private readonly IConferenceStore speakerStore;

        public SpeakersViewModel()
        {
            Title = "Speakers";
            this.speakerStore = DependencyService.Get<IConferenceStore>();
            LoadSpeakers().SafeFireAndForget(false, (ex) => Console.WriteLine(ex));
        }

        private IAsyncCommand reloadSpeakersCommand;
        public IAsyncCommand ReloadSpeakersCommand => reloadSpeakersCommand ?? (reloadSpeakersCommand = new AsyncCommand(LoadSpeakers));

        public async Task LoadSpeakers()
        {
            Speakers = await speakerStore.GetSpeakers().ConfigureAwait(false);
        }

        private IEnumerable<Speaker> speakers;
        public IEnumerable<Speaker> Speakers
        {
            get
            {
                return speakers;
            }
            set
            {
                SetProperty(ref speakers, value);
            }
        }

        private Speaker selectedSpeaker;
        public Speaker SelectedSpeaker
        {
            get { return selectedSpeaker; }
            set
            {
                SetProperty(ref selectedSpeaker, value);
            }
        }

        private IAsyncCommand selectSpeakerCommand;
        public IAsyncCommand SelectSpeakerCommand => selectSpeakerCommand ?? (selectSpeakerCommand = new AsyncCommand(NavigateToSpeaker));

        private async Task NavigateToSpeaker()
        {
            if (SelectedSpeaker == null)
                return;

            var speaker = SelectedSpeaker;
            SelectedSpeaker = null;

            await Shell.Current.Navigation.PushAsync(new SpeakerDetailPage(new SpeakerDetailViewModel(speaker)));
        }
    }
}
