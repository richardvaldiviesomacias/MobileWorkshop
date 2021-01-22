using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileWorkshop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetSummaryView : ContentView
    {
        public BudgetSummaryView()
        {
            InitializeComponent();
            BindingContext = Dependencies.BudgetSummaryViewModel;
            Dependencies.BudgetSummaryViewModel.Width = 230; // TODO: get dynamically after view loads
        }
    }
}