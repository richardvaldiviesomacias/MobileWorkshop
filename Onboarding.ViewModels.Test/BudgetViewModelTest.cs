using FluentAssertions;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.ViewModels.Test
{
    public class BudgetViewModelTest
    {

        [Fact]
        public void Constructor_ValidModel_ExpectPresentation()
        {
            var budget = BudgetBuilder.Build();
            var viewModel = new BudgetViewModel(budget);
            
            viewModel.IncomeGroup.BudgetGroup.Should().Be(budget.IncomeGroup);
            viewModel.HousingGroup.BudgetGroup.Should().Be(budget.HousingGroup);
            viewModel.TransportationGroup.BudgetGroup.Should().Be(budget.TransportationGroup);
            viewModel.FoodGroup.BudgetGroup.Should().Be(budget.FoodGroup);
            viewModel.PersonalGroup.BudgetGroup.Should().Be(budget.PersonalGroup);
            viewModel.GivingGroup.BudgetGroup.Should().Be(budget.GivingGroup);
            viewModel.DebtGroup.BudgetGroup.Should().Be(budget.DebtGroup);
            viewModel.BasicExpensesDisplayGroup.BudgetGroup.Should().Be(budget.BasicExpensesDisplayGroup);
        }
    }
}