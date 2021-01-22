using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Extensions;
using Onboarding.Models;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.ViewModels.Test
{
    public class BudgetSummaryViewModelTest
    {
        [Fact]
        public void Constructor_ValidModel_ExpectPresentation()
        {
            var budget = BudgetBuilder.Build();
            var ratios = new BudgetRatio(budget);
            var summary = new BudgetSummary(budget, ratios);

            var viewModel = new BudgetSummaryViewModel(summary);
            viewModel.Width.Should().Be(summary.Width);
            viewModel.HousingGroupWidth.Should().Be(summary.HousingGroupWidth);
            viewModel.TransportationGroupWidth.Should().Be(summary.TransportationGroupWidth);
            viewModel.FoodGroupWidth.Should().Be(summary.FoodGroupWidth);
            viewModel.PersonalGroupWidth.Should().Be(summary.PersonalGroupWidth);
            viewModel.GivingGroupWidth.Should().Be(summary.GivingGroupWidth);
            viewModel.DebtGroupWidth.Should().Be(summary.DebtGroupWidth);

            viewModel.TransportationGroupOffset.Should().Be(summary.TransportationGroupOffset);
            viewModel.FoodGroupOffset.Should().Be(summary.FoodGroupOffset);
            viewModel.PersonalGroupOffset.Should().Be(summary.PersonalGroupOffset);
            viewModel.GivingGroupOffset.Should().Be(summary.GivingGroupOffset);
            viewModel.DebtGroupOffset.Should().Be(summary.DebtGroupOffset);

            viewModel.BudgetDelta.Should().Be(summary.BudgetDelta.ToCurrencyString());
            viewModel.IsOverBudget.Should().Be(summary.IsOverBudget);
            viewModel.IsUnderBudget.Should().Be(summary.IsUnderBudget);
            viewModel.IsOnBudget.Should().Be(summary.IsOnBudget);
        }

        [Fact]
        public void Width_Set_ExpectSetAndUsedInRatio()
        {
            var budget = BudgetBuilder.Build();
            var ratios = Substitute.ForPartsOf<BudgetRatio>(budget);
            var summary = new BudgetSummary(budget, ratios);
            var viewModel = new BudgetSummaryViewModel(summary);
            ratios.HousingGroupRatio.Returns(1);//what is the difference of using Configure or not
            double width = 200;

            viewModel.Width = width;

            viewModel.Width.Should().Be(width);
            viewModel.HousingGroupWidth.Should().Be(width);
        }

        [Fact]
        public void PropertyChanged_AnyBudgetItemAmountChanged_ExpectABunchOfEvents()
        {
            var budget = BudgetBuilder.Build();
            var ratios = Substitute.ForPartsOf<BudgetRatio>(budget);
            var summary = new BudgetSummary(budget, ratios);
            var viewModel = new BudgetSummaryViewModel(summary);

            List<string> expectedPropertyNames = new List<string>()
            {
                nameof(BudgetSummaryViewModel.HousingGroupWidth),
                nameof(BudgetSummaryViewModel.TransportationGroupWidth),
                nameof(BudgetSummaryViewModel.FoodGroupWidth),
                nameof(BudgetSummaryViewModel.PersonalGroupWidth),
                nameof(BudgetSummaryViewModel.DebtGroupWidth),
                nameof(BudgetSummaryViewModel.GivingGroupWidth),
                nameof(BudgetSummaryViewModel.TransportationGroupOffset),
                nameof(BudgetSummaryViewModel.FoodGroupOffset),
                nameof(BudgetSummaryViewModel.PersonalGroupOffset),
                nameof(BudgetSummaryViewModel.DebtGroupOffset),
                nameof(BudgetSummaryViewModel.GivingGroupOffset),
            };

            List<string> receivedPropertyNames = new List<string>();
            viewModel.PropertyChanged += (o, e) => { receivedPropertyNames.Add(e.PropertyName); };

            budget.IncomeGroup.BudgetItems[0].Amount = 100;

            receivedPropertyNames.Should().Contain(expectedPropertyNames); 
        }
    }
}