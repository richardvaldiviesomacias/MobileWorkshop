using System;
using System.ComponentModel;

namespace Onboarding.Models
{
    public class BudgetSummary
    {
        public const double DefaultWidth = 100;
        readonly Budget budget;
        readonly BudgetRatio budgetRatios;
        
        public event PropertyChangedEventHandler PropertyChanged;
        public double Width { get; set; } = DefaultWidth; // This needs to be set by the view layer via a VM to get valid widths below
        public double HousingGroupWidth => budgetRatios.HousingGroupRatio * Width;
        public double TransportationGroupWidth => budgetRatios.TransportationGroupRatio * Width;
        public double FoodGroupWidth => budgetRatios.FoodGroupRatio * Width;
        public double PersonalGroupWidth => budgetRatios.PersonalGroupRatio * Width;
        public double DebtGroupWidth => budgetRatios.DebtGroupRatio * Width;
        public double GivingGroupWidth => budgetRatios.GivingGroupRatio * Width;

        public double TransportationGroupOffset => HousingGroupWidth;
        public double FoodGroupOffset => TransportationGroupOffset + TransportationGroupWidth;
        public double PersonalGroupOffset => FoodGroupOffset + FoodGroupWidth;
        public double GivingGroupOffset => PersonalGroupOffset + PersonalGroupWidth;
        public double DebtGroupOffset => GivingGroupOffset + GivingGroupWidth;

        public decimal BudgetDelta => Math.Abs(budget.IncomeRemaining);
        public bool IsOverBudget => budget.BudgetState == BudgetState.OverBudget;
        public bool IsOnBudget => budget.BudgetState == BudgetState.OnBudget;
        public bool IsUnderBudget => budget.BudgetState == BudgetState.UnderBudget;

        public BudgetSummary(Budget budget, BudgetRatio ratios)
        {
            this.budget = budget;
            this.budgetRatios = ratios;
            
            budget.PropertyChanged += BudgetChanged;
            budgetRatios.PropertyChanged += RatiosChanged;
        }
        
        public void RatiosChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HousingGroupWidth));
            OnPropertyChanged(nameof(TransportationGroupWidth));
            OnPropertyChanged(nameof(FoodGroupWidth));
            OnPropertyChanged(nameof(PersonalGroupWidth));
            OnPropertyChanged(nameof(DebtGroupWidth));
            OnPropertyChanged(nameof(GivingGroupWidth));

            OnPropertyChanged(nameof(TransportationGroupOffset));
            OnPropertyChanged(nameof(FoodGroupOffset));
            OnPropertyChanged(nameof(PersonalGroupOffset));
            OnPropertyChanged(nameof(DebtGroupOffset));
            OnPropertyChanged(nameof(GivingGroupOffset));
        }

        public void BudgetChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(BudgetDelta));
            OnPropertyChanged(nameof(IsOverBudget));
            OnPropertyChanged(nameof(IsOnBudget));
            OnPropertyChanged(nameof(IsUnderBudget));
        }
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}