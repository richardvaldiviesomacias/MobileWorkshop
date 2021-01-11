using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileWorkshop.Views
{
    [ContentProperty("MainContent")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OneButtonContentView : ContentView
    {
        public Action ContinueButtonAction { get; set; } = () => throw new NotImplementedException();
        public Button ContinueButton => continueButton;

        public View MainContent
        {
            get { return mainContent.Content; }
            set { mainContent.Content = value; }
        }
        
        public OneButtonContentView()
        {
            InitializeComponent();
        }

        private void ContinueButton_OnClicked(object sender, EventArgs e) => ContinueButtonAction?.Invoke();
    }
}