using System.Collections.Generic;

namespace Onboarding.Models.Builders
{
    public static class BudgetBuilder
    {
        //
        // NOTE: we could make this data driven from JSON to provide dynamic, easy-to-update content
        //
        public static BudgetGroup IncomeGroup => new BudgetGroup("1",
            "Income",
            BudgetItemType.Income,
            new List<BudgetItem>()
            {
                new BudgetItem("1", "Paycheck 1", 0, BudgetItemType.Income),
                new BudgetItem("2", "Paycheck 2", 0, BudgetItemType.Income),
            });

        public static BudgetGroup HousingGroup => new BudgetGroup("1",
            "Housing",
            budgetItems: new List<BudgetItem>()
            {
                new BudgetItem("1","Mortgage/Rent"),
                new BudgetItem("2", "Water"),
                new BudgetItem("3", "Natural Gas"),
                new BudgetItem("4", "Electricity"),
                new BudgetItem("5","Cable"),
                new BudgetItem("6","Trash"),
            });

        public static BudgetGroup TransportationGroup => new BudgetGroup("1",
            "Transportation",
            budgetItems: new List<BudgetItem>()
            {
                new BudgetItem("1","Lambo Cleaning"),
                new BudgetItem("2", "Ferrari Repairs"),
            });

        public static BudgetGroup FoodGroup => new BudgetGroup("1",
            "Food",
            budgetItems: new List<BudgetItem>()
            {
                new BudgetItem("1", "Groceries" ),
                new BudgetItem("2", "Restaurants"),
                new BudgetItem("3", "Hard Liquor"),
            });

        public static BudgetGroup PersonalGroup => new BudgetGroup("1", 
            "Personal",
            budgetItems: new List<BudgetItem>()
            {
                new BudgetItem("1", "Clothing"),
                new BudgetItem("2", "Phone"),
                new BudgetItem("3", "Fun Money"),
                new BudgetItem("4", "Hair/Cosmetics"),
            });

        public static BudgetGroup DebtGroup => new BudgetGroup("1",
            "Debt",
            BudgetItemType.Debt,
            new List<BudgetItem>()
            {
                new BudgetItem("1", "Car Payment", 0, BudgetItemType.Debt),
                new BudgetItem("2", "Credit Card 1", 0, BudgetItemType.Debt),
                new BudgetItem("3", "Credit Card 2", 0, BudgetItemType.Debt),
                new BudgetItem("4", "Student Loan", 0, BudgetItemType.Debt),
            });

        public static BudgetGroup GivingGroup => new BudgetGroup("1",
            "Giving",
            budgetItems: new List<BudgetItem>()
            {
                new BudgetItem("1", "Tithe"),
                new BudgetItem("2","Email Phising"),
            });

        // For the basic expenses intro page, we need a display-only view model.  Not synced to remote.
        public static BudgetGroup BasicExpensesDisplayGroup 
            => new BudgetGroup("1",
                "Basic Expenses",
                budgetItems: new List<BudgetItem>()
                {
                    new BudgetItem("1", "Placeholder to store total amount of several other groups")
                });

        public static Budget Build()
            => new Budget(
                IncomeGroup,
                HousingGroup,
                TransportationGroup,
                FoodGroup,
                PersonalGroup,
                GivingGroup,
                DebtGroup,
                BasicExpensesDisplayGroup);          
        
    }
}