using FluentAssertions;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.Models.Test
{
    public class BudgetTest
    {
        [Fact]
        public void Constructor_ExpectPopulateParameters()
        {
            var incomeGroup = new BudgetGroup("","");
            var housingGroup = new BudgetGroup("","");
            var transportationGroup = new BudgetGroup("","");
            var foodGroup = new BudgetGroup("","");
            var personalGroup = new BudgetGroup("","");
            var givingGroup = new BudgetGroup("","");
            var debtGroup = new BudgetGroup("","");
            var basicExpenseDisplayGroup = new BudgetGroup("","");

            var budget = new Budget(incomeGroup, housingGroup, transportationGroup, foodGroup, 
                personalGroup, givingGroup, debtGroup, basicExpenseDisplayGroup);
            
            budget.IncomeGroup.Should().NotBeNull();
            budget.HousingGroup.Should().NotBeNull();
            budget.TransportationGroup.Should().NotBeNull();
            budget.FoodGroup.Should().NotBeNull();
            budget.PersonalGroup.Should().NotBeNull();
            budget.GivingGroup.Should().NotBeNull();
            budget.DebtGroup.Should().NotBeNull();
            budget.BasicExpensesDisplayGroup.Should().NotBeNull();
        }
        
        [Fact]
        public void IncomeRemaining_AllGroupsSet_ExpectNotifyForEveryGroup()
        {
            var budget = BudgetBuilder.Build();
            bool wasNotified = false;
            budget.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(budget.IncomeRemaining)) wasNotified = true;
            };

            budget.IncomeGroup.BudgetItems[0].Amount = 1.23m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.HousingGroup.BudgetItems[0].Amount = 1.23m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.TransportationGroup.BudgetItems[0].Amount = 1.23m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.FoodGroup.BudgetItems[0].Amount = 1.23m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.PersonalGroup.BudgetItems[0].Amount = 1.23m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.DebtGroup.BudgetItems[0].Amount = 1.23m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.GivingGroup.BudgetItems[0].Amount = 1.23m;
            wasNotified.Should().BeTrue();
        }
        
        [Fact]
        public void IncomeRemaining_VariousAmount_CorrectAmount()
        {
            var budget = BudgetBuilder.Build();

            // Some easy ratios for testing
            budget.IncomeGroup.BudgetItems[0].Amount = 100m;
            budget.IncomeRemaining.Should().Be(100);

            budget.HousingGroup.BudgetItems[0].Amount = 1m;
            budget.IncomeRemaining.Should().Be(99);

            budget.TransportationGroup.BudgetItems[0].Amount = 2m;
            budget.IncomeRemaining.Should().Be(97);

            budget.FoodGroup.BudgetItems[0].Amount = 3m;
            budget.IncomeRemaining.Should().Be(94);

            budget.PersonalGroup.BudgetItems[0].Amount = 4m;
            budget.IncomeRemaining.Should().Be(90);

            budget.DebtGroup.BudgetItems[0].Amount = 5m;
            budget.IncomeRemaining.Should().Be(85);

            budget.GivingGroup.BudgetItems[0].Amount = 6m;
            budget.IncomeRemaining.Should().Be(79);
        }
        
        [Fact]
        public void BudgetState_VariousAmount_CorrectState()
        {
            var budget = BudgetBuilder.Build();

            // Some easy ratios for testing
            budget.IncomeGroup.BudgetItems[0].Amount = 100m;
            budget.BudgetState.Should().Be(BudgetState.UnderBudget);

            budget.HousingGroup.BudgetItems[0].Amount = 99m;
            budget.BudgetState.Should().Be(BudgetState.UnderBudget);

            budget.HousingGroup.BudgetItems[0].Amount = 100m;
            budget.BudgetState.Should().Be(BudgetState.OnBudget);

            budget.HousingGroup.BudgetItems[0].Amount = 101m;
            budget.BudgetState.Should().Be(BudgetState.OverBudget);
        }
    }
}