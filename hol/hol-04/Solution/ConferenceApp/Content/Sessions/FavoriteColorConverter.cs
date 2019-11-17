using System;
using System.Globalization;
using Xamarin.Forms;

namespace ConferenceApp.Content.Sessions
{
    public class FavoriteColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool favorite && favorite)
            {
                return (Color) Application.Current.Resources["SignalColor"];
            }

            return Color.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
