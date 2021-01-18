using Xamarin.Forms;

namespace MobileWorkshop.Controls
{
    public class ProcessedEntry : NoUnderlineEntry
    {
        public virtual string DefaultProcessedTextValue => "";

        public static readonly BindableProperty ProcessedTextProperty = BindableProperty.Create(nameof(ProcessedText), typeof(string), typeof(string), propertyChanged: ProcessedTextChanged);

        public string ProcessedText
        {
            get => (string)GetValue(ProcessedTextProperty);
            set => SetValue(ProcessedTextProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (ProcessedText != DefaultProcessedTextValue)
            {
                Text = ProcessedText;
            }
        }

        static void ProcessedTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(newValue == null)
            {
                return;
            }    

            var entry = (ProcessedEntry)bindable;
            entry.ProcessedText = newValue.ToString();
        }
    }
}