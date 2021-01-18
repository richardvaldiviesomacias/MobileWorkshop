using System.Net.Mime;
using Onboarding.Models;
using Xamarin.Forms;

namespace MobileWorkshop.Controls
{
    public class NoUnderlineCurrencyEntry : ProcessedEntry
    {
        bool isProcessing = false;

        public override string DefaultProcessedTextValue => 0m.ToCurrencyString();

        public NoUnderlineCurrencyEntry()
        {
            Unfocused += ProcessText;
        }

        protected override void OnTextChanged(string oldValue, string newValue)
        {
            base.OnTextChanged(oldValue, newValue);

            if (string.IsNullOrEmpty(oldValue) || string.IsNullOrEmpty(newValue) || oldValue == newValue)
            {
                return;
            }

            if (isProcessing)
            {
                return;
            }

            Text = newValue.ToCurrencyStringWhileTyping();
        }

        void ProcessText(object sender, FocusEventArgs e)
        {
            isProcessing = true;

            // Convert text input into currency
            Text = Text.ToDecimalAsCurrency().ToCurrencyString();
            ProcessedText = Text;

            isProcessing = false;
        }
    }
}