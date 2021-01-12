using System.Collections.Generic;
using System.Linq;
using MobileWorkshop.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileWorkshop.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalsPage : ContentPage
    {
        public List<TitledIcon> Goals { get; set; } = new List<TitledIcon>
        {
            new TitledIcon {Id = "own_home", Title = "I Own a Home", ImageSource = "icon_home.png"},
            new TitledIcon {Id = "rent", Title = "I Rent", ImageSource = "icon_home_rent.png"},
            new TitledIcon {Id = "married", Title = "I am Married", ImageSource = "icon_ring.png"},
            new TitledIcon {Id = "kids", Title = "I Own a Home", ImageSource = "icon_pacifier.png"},
            new TitledIcon {Id = "own_car", Title = "I Own a Home", ImageSource = "icon_sport_car.png"},
            new TitledIcon {Id = "own_pets", Title = "I Own a Home", ImageSource = "icon_paw.png"},
        };

        public bool IsContinuedEnabled { get; set; }
        
        public GoalsPage()
        {
            InitializeComponent();
            BindingContext = this;
            contentView.ContinueButton.IsEnabled = false;
            contentView.ContinueButtonAction = () => Navigation.PushAsync(new MenuPage());
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGoals = e.CurrentSelection.Cast<TitledIcon>().ToList();
            Goals.ToList().ForEach(goal => goal.IsSelected = selectedGoals.Contains(goal));
            contentView.ContinueButton.IsEnabled = selectedGoals.Count >= 1;
        }
    }
}