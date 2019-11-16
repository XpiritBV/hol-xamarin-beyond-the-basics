using System;
using AsyncAwaitBestPractices.MVVM;
using Xamarin.Essentials;

namespace ConferenceApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new AsyncCommand(async () => await Launcher.OpenAsync(new Uri("https://xamarin.com/platform")));
        }

        public IAsyncCommand OpenWebCommand { get; }
    }
}