using System;
using System.ComponentModel;

namespace Onboarding.Models
{
    public class BudgetRatio: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public const string RatioChangedProperty = "Ratio";
        readonly Budget budget;
        public BudgetRatio(Budget budget)
        {
            this.budget = budget;
            budget.PropertyChanged += BudgetChanged;
        }

        private void BudgetChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(budget.IncomeRemaining))
            {
                OnPropertyChanged(RatioChangedProperty);
            }
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public virtual double HousingGroupRatio => CalculateGroupRatio(budget.HousingGroup);
        public virtual double TransportationGroupRatio => CalculateGroupRatio(budget.TransportationGroup);
        public virtual double FoodGroupRatio => CalculateGroupRatio(budget.FoodGroup);
        public virtual double PersonalGroupRatio => CalculateGroupRatio(budget.PersonalGroup);
        public virtual double DebtGroupRatio => CalculateGroupRatio(budget.DebtGroup);
        public virtual double GivingGroupRatio => CalculateGroupRatio(budget.GivingGroup);


        double CalculateGroupRatio(BudgetGroup group)
        {
            if (budget.IncomeGroup.TotalAmount <= 0)
            {
                return 1;
            }

            var ratio = (double) (group.TotalAmount / budget.IncomeGroup.TotalAmount);

            ratio = Math.Max(0, ratio);
            ratio = Math.Min(1, ratio);

            return ratio;
        }

    }
}