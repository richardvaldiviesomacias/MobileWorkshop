using System.Globalization;
using System.Text.RegularExpressions;

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
        
        // Taken from the E$ Xam budget project
        // TODO: consider non-regex implementation
        private const string DecimalPattern = @"\d+\.{0,1}\d{0,2}";
        private const string CurrencyCommaPattern = @"(\d)(?=(\d{3})+(\.\d{0,2}){0,1}$)";

        public static string ToCurrencyStringWhileTyping(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            if (input == ".") return "$0.";

            var removeCommas = Regex.Replace(input, ",", "");
            var decimalValue = Regex.Match(removeCommas, DecimalPattern).Value;
            var withoutTheDollarSign = Regex.Replace(decimalValue, CurrencyCommaPattern, "$0,");
            return "$" + withoutTheDollarSign;
        }
    }
}