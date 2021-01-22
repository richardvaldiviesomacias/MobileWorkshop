using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Onboarding.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileWorkshop.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalsPage : ContentPage
    {
        private OnboardingProfileViewModel onboardingProfileViewModel;
        public GoalsPage(OnboardingProfileViewModel onboardingProfileViewModel)
        {
            InitializeComponent();
            this.onboardingProfileViewModel = onboardingProfileViewModel;
            BindingContext = onboardingProfileViewModel;
            contentView.ContinueButton.IsEnabled = false;
            contentView.ContinueButtonAction = async () => await ContinueClicked();
        }

        private async Task ContinueClicked()
        {
            onboardingProfileViewModel.LogSelectedGoals();

            await Navigation.PushAsync(new StatusPage(onboardingProfileViewModel)); 
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedIcons = e.CurrentSelection.Cast<TitledIconViewModel>().ToList();

            // Only set selected to true for items the users has selected in the local view
            onboardingProfileViewModel.Goals.ToList().ForEach(icon => icon.IsSelected = selectedIcons.Contains(icon));

            // Only enable the continue button if more than one item is selected
            contentView.ContinueButton.IsEnabled = selectedIcons.Count >= 1;
        }
    }
}