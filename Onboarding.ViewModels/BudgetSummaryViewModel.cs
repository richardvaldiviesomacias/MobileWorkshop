using System.ComponentModel;
using Onboarding.Models;

namespace Onboarding.ViewModels
{
    public class BudgetSummaryViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly BudgetSummary summary;
        public double Width { 
            get => summary.Width; 
            set => summary.Width = value; 
        }

        public double HousingGroupWidth => summary.HousingGroupWidth;
        public double TransportationGroupWidth => summary.TransportationGroupWidth;
        public double FoodGroupWidth => summary.FoodGroupWidth;
        public double PersonalGroupWidth => summary.PersonalGroupWidth;
        public double DebtGroupWidth => summary.DebtGroupWidth;
        public double GivingGroupWidth => summary.GivingGroupWidth;

        public double TransportationGroupOffset => summary.TransportationGroupOffset;
        public double FoodGroupOffset => summary.FoodGroupOffset;
        public double PersonalGroupOffset => summary.PersonalGroupOffset;
        public double GivingGroupOffset => summary.GivingGroupOffset;
        public double DebtGroupOffset => summary.DebtGroupOffset;

        public string BudgetDelta => summary.BudgetDelta.ToCurrencyString();
        public bool IsOverBudget => summary.IsOverBudget;
        public bool IsOnBudget => summary.IsOnBudget;
        public bool IsUnderBudget => summary.IsUnderBudget;

        public BudgetSummaryViewModel(BudgetSummary summary)
        {
            this.summary = summary;
            summary.PropertyChanged += (o, e) => PropertyChanged?.Invoke(this, e);
        }
    }
}