using ConferenceApp.Contracts.Models;
using ConferenceApp.ViewModels;

namespace ConferenceApp.Content.Speakers
{
    public class SpeakerDetailViewModel : BaseViewModel
    {
        public SpeakerDetailViewModel()
        {
            // only for designer
        }

        public SpeakerDetailViewModel(Speaker item)
        {
            Speaker = item;
            Title = $"{Speaker.FirstName} {Speaker.LastName}";
        }

        private Speaker speaker;
        public Speaker Speaker
        {
            get { return speaker; }
            set { SetProperty(ref speaker, value); }
        }
    }
}
