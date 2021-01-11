using System;
using System.Globalization;
using Xamarin.Forms;

namespace MobileWorkshop.Converters
{
    public class TextDecorationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var textDecorationsConverter = (string) value;

            if (textDecorationsConverter == nameof(TextDecorations.Strikethrough))
            {
                return TextDecorations.Strikethrough;
            }
            else if (textDecorationsConverter == nameof(TextDecorations.Underline))
            {
                return TextDecorations.Underline;
            }

            return TextDecorations.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}