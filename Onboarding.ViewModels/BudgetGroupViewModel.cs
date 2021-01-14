using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Onboarding.Models;
using Onboarding.ViewModels.Annotations;

namespace Onboarding.ViewModels
{
    public class BudgetGroupViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        internal readonly BudgetGroup BudgetGroup;
        readonly BudgetGroupPresentation presentation;
        internal string HeaderTextWithDate => $"{BudgetGroup.Name?.ToUpper()} for {DateTime.Now:MMMM}";
        
        readonly ObservableCollection<BudgetItemViewModel> budgetItems = new ObservableCollection<BudgetItemViewModel>();
        public ObservableCollection<BudgetItemViewModel> BudgetItems => budgetItems;
        public string ColorKey => presentation.ColorKey;
        public string IconSource => presentation.IconSource;
        public string Description => presentation.Description;
        public string Subtitle => presentation.Subtitle;
        public string AddItemText => presentation.AddItemText;
        public string CustomTitle => presentation.CustomTitle;
        public bool IsTitleVisible => string.IsNullOrEmpty(CustomTitle);
        public bool IsCustomTitleVisible => !IsTitleVisible;
        public bool HasSecondaryHeader => presentation.HasSecondaryHeader;
        public string HeaderTextWithoutDate => BudgetGroup.Name?.ToUpper();
        public string HeaderText  =>
            presentation.HasDateOnHeader
                ? HeaderTextWithDate
                : HeaderTextWithoutDate;

        public string Name => BudgetGroup.Name;
        public string NameLower => BudgetGroup.Name.ToLower();
        public string TotalAmount => BudgetGroup.TotalAmount.ToCurrencyString();
        public bool IsTotalAmountZero => BudgetGroup.TotalAmount == 0;
  

        public BudgetGroupViewModel(BudgetGroup budgetGroup)
        {
            BudgetGroup = budgetGroup;
            presentation = new BudgetGroupPresentation();
            budgetGroup.BudgetItems.CollectionChanged += BudgetItemChanged;
            budgetGroup.BudgetItems.ToList().ForEach(item =>
            {
                var viewModel = new BudgetItemViewModel(item);
                BudgetItems.Add(viewModel);
            });
            budgetGroup.PropertyChanged += (o, e) => PropertyChanged?.Invoke(this, e); 
        }

        private void BudgetItemChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            e.NewItems?.Cast<BudgetItem>().ToList().ForEach(item =>
            {
                var viewModel = new BudgetItemViewModel(item);
                BudgetItems.Add(viewModel);
            });
            
            e.OldItems?.Cast<BudgetItem>().ToList().ForEach(item => BudgetItems.Remove(FindMatchingViewModel(item)));
        }

        private BudgetItemViewModel FindMatchingViewModel(BudgetItem item) =>
            BudgetItems.Where(vm => vm.BudgetItem == item).FirstOrDefault();

        
    }
}