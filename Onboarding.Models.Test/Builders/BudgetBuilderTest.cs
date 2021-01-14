using FluentAssertions;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.Models.Test.Builders
{
    public class BudgetBuilderTest
    {
        [Fact]
        public void Build_ExpectPopulatedBudget()
        {
            var budget = BudgetBuilder.Build();

            budget.IncomeGroup.Should().NotBeNull();
            budget.HousingGroup.Should().NotBeNull();
            budget.TransportationGroup.Should().NotBeNull();
            budget.FoodGroup.Should().NotBeNull();
            budget.PersonalGroup.Should().NotBeNull();
            budget.GivingGroup.Should().NotBeNull();
            budget.DebtGroup.Should().NotBeNull();
            budget.BasicExpensesDisplayGroup.Should().NotBeNull();
        }
    }
}