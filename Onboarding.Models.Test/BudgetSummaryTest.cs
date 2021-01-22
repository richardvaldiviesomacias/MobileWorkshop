using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.Models.Test
{
    public class BudgetSummaryTest
    {
        [Fact]
        public void Constructor_ValidModel_ExpectPresentation()
        {
            var budget = BudgetBuilder.Build();
            var ratios = new BudgetRatio(budget);

            var summary = new BudgetSummary(budget, ratios);
            summary.HousingGroupWidth.Should().Be(summary.Width);
            summary.TransportationGroupWidth.Should().Be(summary.Width);
            summary.FoodGroupWidth.Should().Be(summary.Width);
            summary.PersonalGroupWidth.Should().Be(summary.Width);
            summary.GivingGroupWidth.Should().Be(summary.Width);
            summary.DebtGroupWidth.Should().Be(summary.Width);

            summary.TransportationGroupOffset.Should().Be(summary.HousingGroupWidth);
            summary.FoodGroupOffset.Should().Be(summary.TransportationGroupOffset + summary.TransportationGroupWidth);
            summary.PersonalGroupOffset.Should().Be(summary.FoodGroupOffset + summary.FoodGroupWidth);
            summary.GivingGroupOffset.Should().Be(summary.PersonalGroupOffset + summary.PersonalGroupWidth);
            summary.DebtGroupOffset.Should().Be(summary.GivingGroupOffset + summary.GivingGroupWidth);

            summary.BudgetDelta.Should().Be(0m);
            summary.IsOverBudget.Should().Be(false);
            summary.IsUnderBudget.Should().Be(false);
            summary.IsOnBudget.Should().Be(true);
        }
        
               [Fact]
        public void Constructor_NullBudget_ExpectException()
        {
            var budget = BudgetBuilder.Build();
            var ratios = new BudgetRatio(budget);

            // TEST: construct view model
            Action testAction = () => new BudgetSummary(null, ratios);

            testAction.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_NullRatios_ExpectException()
        {
            var budget = BudgetBuilder.Build();

            // TEST: construct view model
            Action testAction = () => new BudgetSummary(budget, null);

            testAction.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Width_Set_ExpectSetAndUsedInRatios()
        {
            var budget = BudgetBuilder.Build();
            var ratios = Substitute.ForPartsOf<BudgetRatio>(budget);
            var summary = new BudgetSummary(budget, ratios);
            ratios.HousingGroupRatio.Returns(1);
            double width = 200;

            // TEST: new width
            summary.Width = width;

            summary.Width.Should().Be(width);
            summary.HousingGroupWidth.Should().Be(width);
        }

        [Fact]
        public void HousingGroupWidth_VariousWidthsAndRatios_MatchesExpectResult()
        {
            double ratio = 0.5;
            var budget = BudgetBuilder.Build();
            var ratios = Substitute.ForPartsOf<BudgetRatio>(budget);
            var summary = new BudgetSummary(budget, ratios);

            ratios.HousingGroupRatio.Returns(ratio);
            summary.HousingGroupWidth.Should().Be(summary.Width * ratio);
        }

        [Fact]
        public void HousingGroupWidth_MoreWidthsAndRatios_MatchesExpectResult()
        {
            var budget = BudgetBuilder.Build();
            var ratios = new BudgetRatio(budget);
            var summary = new BudgetSummary(budget, ratios);

            budget.IncomeGroup.BudgetItems[0].Amount = 100;
            budget.HousingGroup.BudgetItems[0].Amount = 75;
            budget.FoodGroup.BudgetItems[0].Amount = 25;

            summary.HousingGroupWidth.Should().Be(summary.Width * 0.75);
            summary.FoodGroupWidth.Should().Be(summary.Width * 0.25);
            summary.FoodGroupOffset.Should().Be(summary.HousingGroupWidth);
        }

        // TODO: this should be a theory testing all budget groups,
        [Fact]
        public void PropertyChanged_AnyBudgetItemAmountChanged_ExpectABunchOfEevents()
        {
            var budget = BudgetBuilder.Build();
            var ratios = Substitute.ForPartsOf<BudgetRatio>(budget);
            var summary = new BudgetSummary(budget, ratios);

            List<string> expectedPropertyNames = new List<string>()
            {
                nameof(BudgetSummary.HousingGroupWidth),
                nameof(BudgetSummary.TransportationGroupWidth),
                nameof(BudgetSummary.FoodGroupWidth),
                nameof(BudgetSummary.PersonalGroupWidth),
                nameof(BudgetSummary.DebtGroupWidth),
                nameof(BudgetSummary.GivingGroupWidth),
                nameof(BudgetSummary.TransportationGroupOffset),
                nameof(BudgetSummary.FoodGroupOffset),
                nameof(BudgetSummary.PersonalGroupOffset),
                nameof(BudgetSummary.DebtGroupOffset),
                nameof(BudgetSummary.GivingGroupOffset),
            };

            List<string> receivedPropertyNames = new List<string>();
            summary.PropertyChanged += (o, e) => { receivedPropertyNames.Add(e.PropertyName); };

            budget.IncomeGroup.BudgetItems[0].Amount = 100;

            receivedPropertyNames.Should().Contain(expectedPropertyNames);
        }

        [Fact]
        public void PropertyChanged_AnyBudgetItemAmountChanged_ExpectBudgetEvents()
        {
            var budget = BudgetBuilder.Build();
            var ratios = Substitute.ForPartsOf<BudgetRatio>(budget);
            var summary = new BudgetSummary(budget, ratios);

            List<string> expectedPropertyNames = new List<string>()
            {
                nameof(BudgetSummary.BudgetDelta),
                nameof(BudgetSummary.IsOverBudget),
                nameof(BudgetSummary.IsOnBudget),
                nameof(BudgetSummary.IsUnderBudget),
            };

            List<string> receivedPropertyNames = new List<string>();
            summary.PropertyChanged += (o, e) => { receivedPropertyNames.Add(e.PropertyName); };

            budget.IncomeGroup.BudgetItems[0].Amount = 100;

            receivedPropertyNames.Should().Contain(expectedPropertyNames);
        }
    }
}