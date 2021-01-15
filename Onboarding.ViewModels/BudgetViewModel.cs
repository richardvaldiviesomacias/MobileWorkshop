using System;
using Onboarding.Models;

namespace Onboarding.ViewModels
{
    public class BudgetViewModel
    {
        public readonly Budget Budget;
        public readonly BudgetGroupViewModel IncomeGroup;
        public readonly BudgetGroupViewModel HousingGroup;
        public readonly BudgetGroupViewModel TransportationGroup;
        public readonly BudgetGroupViewModel FoodGroup;
        public readonly BudgetGroupViewModel PersonalGroup;
        public readonly BudgetGroupViewModel GivingGroup;
        public readonly BudgetGroupViewModel DebtGroup;
        public readonly BudgetGroupViewModel BasicExpensesDisplayGroup;
        public BudgetViewModel(Budget budget)
        {
            Budget = budget ?? throw new ArgumentNullException(nameof(budget));
            IncomeGroup = new BudgetGroupViewModel(budget.IncomeGroup);
            HousingGroup = new BudgetGroupViewModel(budget.HousingGroup);
            TransportationGroup = new BudgetGroupViewModel(budget.TransportationGroup);
            FoodGroup = new BudgetGroupViewModel(budget.FoodGroup);
            PersonalGroup = new BudgetGroupViewModel(budget.PersonalGroup);
            GivingGroup = new BudgetGroupViewModel(budget.GivingGroup);
            DebtGroup = new BudgetGroupViewModel(budget.DebtGroup);
            BasicExpensesDisplayGroup = new BudgetGroupViewModel(budget.BasicExpensesDisplayGroup);
        }

       
    }
}