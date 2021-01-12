using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileWorkshop.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomePage : ContentPage
    {
        public BudgetGroup BudgetGroup { get; set; } = new BudgetGroup
        {
           BudgetItems = new ObservableCollection<BudgetItem>
           {
               new BudgetItem { Name = "Item 1", Amount = "22.22"},
               new BudgetItem { Name = "Item 2", Amount = "111.11"},
           }
        };
        public IncomePage()
        {
            InitializeComponent();
            BindingContext = BudgetGroup;
            contentView.ContinueButtonAction = () => Navigation.PushAsync(new IntroPage());
        }
    }

    public class BudgetItem
    {
        public string Name { get; set; }
        public string Amount { get; set; }
    }

    public class BudgetGroup
    {
        public string Name => "Income";
        public string NameLower => Name.ToLower();
        public string Subtitle => "This is where a witty subtitle goes";
        public string IconSource => "icon_income.png";
        public string HeaderText => $"{Name} for THIS MONTH";

        public ObservableCollection<BudgetItem> BudgetItems { get; set; } = new ObservableCollection<BudgetItem>();
    }
}