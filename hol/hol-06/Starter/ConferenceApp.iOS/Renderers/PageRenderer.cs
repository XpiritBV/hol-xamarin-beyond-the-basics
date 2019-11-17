using System;
using ConferenceApp.Services;
using ConferenceApp.Styles;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ConferenceApp.iOS.Renderers.PageRenderer))]
namespace ConferenceApp.iOS.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                ThemeService.ApplyTheme(CurrentTheme());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"\t\t\tERROR: {ex.Message}");
            }
        }

        /// <summary>
        /// Called when the user changes dark/light mode while the app is running
        /// </summary>
        /// <param name="previousTraitCollection"></param>
        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (TraitCollection.UserInterfaceStyle != previousTraitCollection.UserInterfaceStyle)
            {
                Console.WriteLine($"TraitCollectionDidChange: {TraitCollection.UserInterfaceStyle} != {previousTraitCollection.UserInterfaceStyle}");
                ThemeService.ApplyTheme(CurrentTheme());
            }
        }

        private string CurrentTheme()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                return TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark
                    ? nameof(DarkTheme)
                    : nameof(LightTheme);
            }

            return nameof(LightTheme);
        }
    }
}
