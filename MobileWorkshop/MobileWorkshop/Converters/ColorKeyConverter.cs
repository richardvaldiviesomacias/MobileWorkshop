using System;
using System.Globalization;
using Xamarin.Forms;

namespace MobileWorkshop.Converters
{
    public class ColorKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var key = value.ToString();
                var color = Application.Current.Resources.GetColor(key);
                return color;
            }
            catch
            {
                // TODO: Log exception here
                return ResourcesExtensions.DefaultColor;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return value;
            throw new NotImplementedException();
        }
    }
}