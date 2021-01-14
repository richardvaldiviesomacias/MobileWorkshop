using System.ComponentModel;

namespace Onboarding.Models
{
    public class BudgetItem: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }

        public BudgetItemType Type { get; }

        public BudgetItem(string id, 
            string name, 
            decimal amount = 0m, 
            BudgetItemType type = BudgetItemType.Expense)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Type = type;
        }

    }

    public enum BudgetItemType
    {
        Income,
        Expense,
        Debt
    }
}