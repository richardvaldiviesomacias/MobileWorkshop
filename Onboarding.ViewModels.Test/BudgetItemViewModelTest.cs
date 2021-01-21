using System;
using FluentAssertions;
using Onboarding.Models;
using Onboarding.RemoteBudget;
using Xunit;
using BudgetItem = Onboarding.Models.BudgetItem;

namespace Onboarding.ViewModels.Test
{
    public class BudgetItemViewModelTest
    {
        [Fact]
        public void Constructor_ValidModel_ExpectPresentation()
        {
            var budgetItem = new BudgetItem("id", "name", 100.42m, BudgetItemType.Debt);
            var viewModel = new BudgetItemViewModel(budgetItem);
            
            viewModel.Name.Should().Be(budgetItem.Name);
            viewModel.Amount.Should().Be(budgetItem.Amount.ToCurrencyString());
        }

        [Fact]
        public void Constructor_NullModel_ExpectToScreamLikePig()
        {
            Action testAction = () => new BudgetItemViewModel(null);
            testAction.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AmountNegative_ExpectPositiveString()
        {
            decimal value = -100m;
            var viewModel = new BudgetItemViewModel(new BudgetItem("id", "name", value, BudgetItemType.Debt));
            string expectedValue = Math.Abs(value).ToCurrencyString();
            viewModel.Amount.Should().Be(expectedValue);
        }

        [Fact]
        public void Amount_Set_ExpectPropertyChanged()
        {
            var budgetItem = new BudgetItem("id", "name");
            var viewModel = new BudgetItemViewModel(budgetItem);

            decimal newAmount = -220m;
            bool wasChanged = false;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(viewModel.Amount))
                {
                    wasChanged = true;
                }
            };

            viewModel.Amount = newAmount.ToCurrencyString();
            wasChanged.Should().BeTrue();
        }
        
        [Fact]
        public void Name_Set_ExpectPropertyChanged()
        {
            var budgetItem = new BudgetItem("id", "name");
            var viewModel = new BudgetItemViewModel(budgetItem);

            string newName = "new name";
            bool wasChanged = false;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(viewModel.Name))
                {
                    wasChanged = true;
                }
            };

            viewModel.Name = newName;
            wasChanged.Should().BeTrue();
        }
    }
}