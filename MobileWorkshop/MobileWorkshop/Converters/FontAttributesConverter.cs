using System;
using System.Globalization;
using Xamarin.Forms;

namespace MobileWorkshop.Converters
{
    public class FontAttributesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fontAttributesString = (string) value;

            if (fontAttributesString == nameof(FontAttributes.Bold))
            {
                return FontAttributes.Bold;
            }
            else if (fontAttributesString == nameof(FontAttributes.Italic))
            {
                return FontAttributes.Italic;
            }

            return FontAttributes.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}