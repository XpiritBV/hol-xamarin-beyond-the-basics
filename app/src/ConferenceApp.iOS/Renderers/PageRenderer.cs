using System;
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
                SetAppTheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"\t\t\tERROR: {ex.Message}");
            }
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (TraitCollection.UserInterfaceStyle != previousTraitCollection.UserInterfaceStyle)
            {
                Console.WriteLine($"TraitCollectionDidChange: {TraitCollection.UserInterfaceStyle} != {previousTraitCollection.UserInterfaceStyle}");
                SetAppTheme();
            }
        }

        void SetAppTheme()
        {
            if (TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark)
            {
                if (App.AppTheme == nameof(DarkTheme))
                    return;

                //Add a Check for App Theme since this is called even when not changed really
                Xamarin.Forms.Application.Current.Resources = new DarkTheme();
                App.AppTheme = nameof(DarkTheme);
            }
            else
            {
                if (App.AppTheme == nameof(LightTheme))
                    return;
                Xamarin.Forms.Application.Current.Resources = new LightTheme();
                App.AppTheme = nameof(LightTheme);
            }
        }
    }
}
