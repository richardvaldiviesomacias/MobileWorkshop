using Onboarding.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileWorkshop.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransportationPage : ContentPage
    {
        public TransportationPage(BudgetGroupViewModel budgetGroupViewModel)
        {
            InitializeComponent();
            BindingContext = budgetGroupViewModel;

            contentView.ContinueButtonAction = async () => await Navigation.PushAsync(new IntroPage());
        }
    }
}