using FluentAssertions;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.Models.Test.Builders
{
    public static class Constants
    {
        public const string Income = "Income";
        public const string Housing = "Housing";
        public const string Transportation = "Transportation";
        public const string Food = "Food";
        public const string Personal = "Personal";
        public const string Debt = "Debt";
        public const string Giving = "Giving";
        public const string BasicExpenses = "Basic Expenses";
    } 
    
    public class BudgetGroupPresentationBuilderTest
    {
        [Theory]
        [InlineData("1", Constants.Income)]
        [InlineData("2", Constants.Housing)]
        [InlineData("3", Constants.Transportation)]
        [InlineData("4", Constants.Food)]
        [InlineData("5", Constants.Personal)]
        [InlineData("6", Constants.Debt)]
        [InlineData("7", Constants.Giving)]
        [InlineData("8", Constants.BasicExpenses)]
        public void Constructor_ValidParameter_PresentationSetupCorrectly(string id, string groupName)
        {
            var group = new BudgetGroup(id, groupName);
            var expectedPresentation = BudgetGroupPresentationBuilder.IncomeGroupPresentation;
            
            if (groupName == Constants.Housing)
            {
                expectedPresentation = BudgetGroupPresentationBuilder.HousingGroupPresentation;
            }

            if (groupName == Constants.Transportation)
            {
                expectedPresentation = BudgetGroupPresentationBuilder.TransportationGroupPresentation;
            }

            if (groupName == Constants.Food)
            {
                expectedPresentation = BudgetGroupPresentationBuilder.FoodGroupPresentation;
            }
            
            if (groupName == Constants.Personal)
            {
                expectedPresentation = BudgetGroupPresentationBuilder.PersonalGroupPresentation;
            }

            if (groupName == Constants.Debt)
            {
                expectedPresentation = BudgetGroupPresentationBuilder.DebtGroupPresentation;
            }
            
            if (groupName == Constants.Giving)
            {
                expectedPresentation = BudgetGroupPresentationBuilder.GivingGroupPresentation;
            }
            
            if (groupName == Constants.BasicExpenses)
            {
                expectedPresentation = BudgetGroupPresentationBuilder.BasicExpensesDisplayGroup;
            }
            
            // TEST: Construct a view model
            var presentation = BudgetGroupPresentationBuilder.Build(group);

            presentation.Should().BeEquivalentTo(expectedPresentation);
        }
    }
}