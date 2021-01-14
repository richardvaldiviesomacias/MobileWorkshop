using System.Globalization;

namespace Onboarding.Models
{
    public static class Extensions
    {
        public static string ToCurrencyString(this decimal value)
        {
            return value.ToString("C");
        }
        public static decimal ToDecimalAsCurrency(this string value)
        {
            decimal.TryParse(value, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, null, out decimal result);
            return result;
        }
    }
}