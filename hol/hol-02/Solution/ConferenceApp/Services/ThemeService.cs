using System;
using ConferenceApp.Styles;
using Xamarin.Forms;

namespace ConferenceApp.Services
{
    public static class ThemeService
    {
        static string appTheme = nameof(LightTheme);

        public static void ApplyTheme(string theme)
        {
            try
            {
                switch (theme)
                {
                    case nameof(DarkTheme):
                        {
                            if (appTheme == nameof(DarkTheme))
                                return;

                            Application.Current.Resources = new DarkTheme();
                            appTheme = nameof(DarkTheme);
                        }
                        break;
                    case nameof(LightTheme):
                        {
                            if (appTheme == nameof(LightTheme))
                                return;

                            Application.Current.Resources = new LightTheme();
                            appTheme = nameof(LightTheme);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\t\t\tERROR applying theme {theme}: {ex.Message}");
            }
        }
    }
}
