using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Onboarding.Models
{
    public class BudgetGroup: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public readonly string Id;
        public readonly string Name;
        //public readonly BudgetGroupPresentation Presentation;
        public readonly BudgetItemType DefaultItemType;
        public readonly ObservableCollection<BudgetItem> BudgetItems = new ObservableCollection<BudgetItem>();
        public decimal TotalAmount => BudgetItems.Sum(i => i.Amount);

        public BudgetGroup(string id, 
            string name,
            BudgetItemType defaultItemType = BudgetItemType.Expense,
            List<BudgetItem> budgetItems = null)
        {
  
            Id = id;
            Name = name;
            DefaultItemType = defaultItemType;

            BudgetItems.CollectionChanged += BudgetItemCollectionChanged;

            budgetItems?.ForEach(item => BudgetItems.Add(item));
        }

        private void BudgetItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Add a listener to each new group
            var newsItemsList = e.NewItems?.Cast<BudgetItem>().ToList();
            newsItemsList?.ForEach(a => a.PropertyChanged += BudgetItemPropertyChanged);
            NotifyTotalAmountChanged();
        }

        private void BudgetItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Notify listeners of amount changes
            if (e.PropertyName == nameof(BudgetItem.Amount))
            {
                NotifyTotalAmountChanged();
            }
        }
        
        private void NotifyTotalAmountChanged()
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalAmount)));
    }
}