using System;
using System.Globalization;
using Xamarin.Forms;

namespace MobileWorkshop.Converters
{
    public class ActionToCommandConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var commandAction = (Action) value ?? throw new ArgumentException("Value must be an Action");

            return new Command(execute: commandAction);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}