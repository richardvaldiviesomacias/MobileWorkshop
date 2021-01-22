using FluentAssertions;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.Models.Test
{
    public class BudgetRatioTest
    {
        [Fact]
        public void GroupRatios_ZeroValues_ExpectOneRatios()
        {
            var budget = BudgetBuilder.Build();
            var ratios = new BudgetRatio(budget);
            
            //some ratios
            budget.IncomeGroup.BudgetItems[0].Amount = 0;
            budget.HousingGroup.BudgetItems[0].Amount = -1;
            budget.TransportationGroup.BudgetItems[0].Amount = 123.4m;
            budget.FoodGroup.BudgetItems[0].Amount = 0;
            budget.PersonalGroup.BudgetItems[0].Amount = 0;
            budget.DebtGroup.BudgetItems[0].Amount = 0;
            budget.GivingGroup.BudgetItems[0].Amount = 0;

            ratios.HousingGroupRatio.Should().Be(1);
            ratios.TransportationGroupRatio.Should().Be(1);
            ratios.FoodGroupRatio.Should().Be(1);
            ratios.PersonalGroupRatio.Should().Be(1);
            ratios.DebtGroupRatio.Should().Be(1);
            ratios.GivingGroupRatio.Should().Be(1);
        }

        [Fact]
        public void GroupRatios_VariousAmount_CorrectRatios()
        {
            var budget = BudgetBuilder.Build();
            var ratios = new BudgetRatio(budget);

            budget.IncomeGroup.BudgetItems[0].Amount = 100m;
            budget.HousingGroup.BudgetItems[0].Amount = 1m;
            budget.TransportationGroup.BudgetItems[0].Amount = 2m;
            budget.FoodGroup.BudgetItems[0].Amount = 3m;
            budget.PersonalGroup.BudgetItems[0].Amount = 4m;
            budget.DebtGroup.BudgetItems[0].Amount = 5m;
            budget.GivingGroup.BudgetItems[0].Amount = 6m;

            ratios.HousingGroupRatio.Should().Be(0.01);
            ratios.TransportationGroupRatio.Should().Be(0.02);
            ratios.FoodGroupRatio.Should().Be(0.03);
            ratios.PersonalGroupRatio.Should().Be(0.04);
            ratios.DebtGroupRatio.Should().Be(0.05);
            ratios.GivingGroupRatio.Should().Be(0.06);
        }

        [Fact] public void GroupRatios_HalfwayAmount_ExpectOneHalfRatio()
        {
            var budget = BudgetBuilder.Build();
            var ratios = new BudgetRatio(budget);

            budget.IncomeGroup.BudgetItems[0].Amount = 200m;
            budget.HousingGroup.BudgetItems[1].Amount = 100;
            ratios.HousingGroupRatio.Should().Be(0.5);
        }

        [Fact]
        public void GroupRatios_FullAmount_ExpectOneRatio()
        {
            var budget = BudgetBuilder.Build();
            var ratios = new BudgetRatio(budget);

            budget.IncomeGroup.BudgetItems[0].Amount = 200m;
            budget.HousingGroup.BudgetItems[0].Amount = 200m;
            ratios.HousingGroupRatio.Should().Be(1);
        }

        [Fact]
        public void PropertyChanged_BudgetGroupAmountChanged_ExpectRatioChangedEvent()
        {
            var budget = BudgetBuilder.Build();
            var ratios = new BudgetRatio(budget);

            bool wasNotified = false;
            ratios.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == BudgetRatio.RatioChangedProperty) wasNotified = true;
            };

            budget.IncomeGroup.BudgetItems[0].Amount = 100m;
            wasNotified.Should().BeTrue();
            
            wasNotified = false;
            budget.HousingGroup.BudgetItems[0].Amount = 1m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.TransportationGroup.BudgetItems[0].Amount = 1m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.FoodGroup.BudgetItems[0].Amount = 1m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.PersonalGroup.BudgetItems[0].Amount = 1m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.DebtGroup.BudgetItems[0].Amount = 1m;
            wasNotified.Should().BeTrue();

            wasNotified = false;
            budget.GivingGroup.BudgetItems[0].Amount = 1m;
            wasNotified.Should().BeTrue();
        }
    }
}