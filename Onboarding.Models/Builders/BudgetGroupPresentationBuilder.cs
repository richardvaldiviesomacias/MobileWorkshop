using System;

namespace Onboarding.Models.Builders
{
    public class BudgetGroupPresentationBuilder
    {
        public static BudgetGroupPresentation IncomeGroupPresentation => new BudgetGroupPresentation(
            "OnboardingGreen",
            "icon_income.png",
            "A budget starts with your income. It is your most powerful tool to achieve goals.",
            "It's okay if these fluctuate each month.",
            "+ Add Paycheck",
            "Enter your paycheck amounts.",
            hasSecondaryHeader: true
        );

        public static BudgetGroupPresentation HousingGroupPresentation => new BudgetGroupPresentation(
            iconSource: "icon_housing.png"
        );

        public static BudgetGroupPresentation TransportationGroupPresentation => new BudgetGroupPresentation(
            "OnboardingOrange", "icon_auto.png", hasSecondaryHeader: true
        );
        
        public static BudgetGroupPresentation FoodGroupPresentation
            => new BudgetGroupPresentation("OnboardingDeepBlue", "icon_food.png");

        public static BudgetGroupPresentation PersonalGroupPresentation
            => new BudgetGroupPresentation("OnboardingPurple", "icon_personal.png");

        public static BudgetGroupPresentation DebtGroupPresentation
            => new BudgetGroupPresentation(
                "OnboardingRed",
                "icon_debt.png",
                "You can't build wealth or leave a legacy with debt.   It's time to pay it all off!  Start by lasting every debt in your budget.");

        public static BudgetGroupPresentation GivingGroupPresentation
            => new BudgetGroupPresentation(
                "OnboardingLightPurple",
                "icon_giving.png",
                "Giving is an important part of a healthy budget, no matter what your money goals are.");

        // For the basic expenses intro page, we need a display-only view model.  Not synced to remote.
        public static BudgetGroupPresentation BasicExpensesDisplayGroup
            => new BudgetGroupPresentation(
                "OnboardingBlue",
                "icon_housing.png",
                "Let's start with the basics-food, utilities and shelter (housing), and transportation.  Then all the rest.");
        
        public static BudgetGroupPresentation Build(BudgetGroup group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            var budget = BudgetBuilder.Build();
            // TODO: a map pattern may be helpful here instead of the long if/if else/else, with names or other id as constants
            if (group.Name == budget.IncomeGroup.Name)
            {
                return IncomeGroupPresentation;
            }

            if (@group.Name == budget.HousingGroup.Name)
            {
                return HousingGroupPresentation;
            }

            if (@group.Name == budget.TransportationGroup.Name)
            {
                return TransportationGroupPresentation;
            }

            if (@group.Name == budget.FoodGroup.Name)
            {
                return FoodGroupPresentation;
            }

            if (@group.Name == budget.PersonalGroup.Name)
            {
                return PersonalGroupPresentation;
            }

            if (@group.Name == budget.DebtGroup.Name)
            {
                return DebtGroupPresentation;
            }

            if (@group.Name == budget.GivingGroup.Name)
            {
                return GivingGroupPresentation;
            }

            if (@group.Name == budget.BasicExpensesDisplayGroup.Name)
            {
                return BasicExpensesDisplayGroup;
            }

            throw new ArgumentException($"Group `{@group.Name}` was not in budget or does not have a presentation");
        }
    }
}