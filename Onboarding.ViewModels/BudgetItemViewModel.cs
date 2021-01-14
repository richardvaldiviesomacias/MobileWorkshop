using System;
using System.ComponentModel;
using Onboarding.Models;

namespace Onboarding.ViewModels
{
    public class BudgetItemViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal readonly BudgetItem BudgetItem;

        public string Name
        {
            get => BudgetItem.Name;
            set => BudgetItem.Name = value;
        }

        public string Amount
        {
            get => Math.Abs(BudgetItem.Amount).ToCurrencyString();
            set => BudgetItem.Amount = value.ToDecimalAsCurrency();
        }

        public BudgetItemViewModel(BudgetItem budgetItem)
        {
            BudgetItem = budgetItem ?? throw new ArgumentNullException(nameof(budgetItem));
            budgetItem.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }
        
        
    }
}