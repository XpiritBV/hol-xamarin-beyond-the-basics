using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using ConferenceApp.Contracts;
using ConferenceApp.Contracts.Models;
using ConferenceApp.Services;
using Shiny;
using Xamarin.Forms;

namespace ConferenceApp.Content.Speakers
{
    public class SpeakersViewModel : ViewModels.BaseViewModel
    {
        private readonly IConferenceStore speakerStore;
        private readonly ISyncService syncService;

        public SpeakersViewModel()
        {
            Title = "Speakers";
            speakerStore = ShinyHost.Resolve<IConferenceStore>();
            syncService = ShinyHost.Resolve<ISyncService>();
            LoadSpeakers().SafeFireAndForget(false, (ex) => Console.WriteLine(ex));
        }

        private IAsyncCommand reloadSpeakersCommand;
        public IAsyncCommand ReloadSpeakersCommand => reloadSpeakersCommand ?? (reloadSpeakersCommand = new AsyncCommand(LoadSpeakers));

        public async Task LoadSpeakers()
        {
            IsBusy = true;

            try
            {
                await syncService.SyncIfNecessary(CancellationToken.None).ConfigureAwait(false);
                Speakers = await speakerStore.GetSpeakers().ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
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
            try
            {
                if (SelectedSpeaker == null)
                    return;

                await Shell.Current.GoToAsync($"speakerdetail?speakerId={SelectedSpeaker.Id}");

                SelectedSpeaker = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
