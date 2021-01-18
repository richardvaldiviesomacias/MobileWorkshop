using Onboarding.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileWorkshop.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomePage : ContentPage
    {
        public IncomePage(BudgetGroupViewModel budgetGroupViewModel)
        {
            InitializeComponent();
            BindingContext = budgetGroupViewModel;
            contentView.ContinueButtonAction = () => Navigation.PushAsync(new IntroPage());
        }
    }
    
}