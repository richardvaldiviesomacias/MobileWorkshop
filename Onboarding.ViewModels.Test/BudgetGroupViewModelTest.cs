using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Onboarding.Models;
using Onboarding.RemoteBudget;
using Xunit;
using Xunit.Sdk;
using BudgetGroup = Onboarding.Models.BudgetGroup;
using BudgetItem = Onboarding.Models.BudgetItem;

namespace Onboarding.ViewModels.Test
{
    public class BudgetGroupViewModelTest
    {
        [Fact]
        public void Constructor_DefaultBudgetGroupPresentation_PresentsDefaultValues()
        {
            var name = "group";
            var group = new BudgetGroup("group1", name);

            BudgetGroupViewModel viewModel = new BudgetGroupViewModel(group);

            viewModel.BudgetItems.Should().BeEmpty();
            viewModel.ColorKey.Should().Be(BudgetGroupPresentation.DefaultColorKey);
            viewModel.IconSource.Should().Be("");
            viewModel.Description.Should().Be("");
            viewModel.Subtitle.Should().Be(BudgetGroupPresentation.DefaultSubtitle);
            viewModel.AddItemText.Should().Be(BudgetGroupPresentation.DefaultAddItemText);
            viewModel.CustomTitle.Should().Be("");
            viewModel.IsTitleVisible.Should().BeTrue("a custom title is not provided");
            viewModel.IsCustomTitleVisible.Should().BeFalse("a custom title is not provided");
            viewModel.HasSecondaryHeader.Should().BeFalse();
            viewModel.HeaderTextWithoutDate.Should().Be(name.ToUpper());
            viewModel.HeaderText.Should().Be(viewModel.HeaderTextWithoutDate,"hasDateOnHeader is false" );
        }

        [Fact]
        public void Constructor_ModelWithBudgetItems_PresentsGroupAndItems()
        {
            string name = "test group name";
            var items = new List<BudgetItem>
            {
                new BudgetItem("id1", "group1"),
                new BudgetItem("id2", "group2", 12.34m),
            };

            var group = new BudgetGroup("id3", name, budgetItems: items);
            var viewModel = new BudgetGroupViewModel(group);

            viewModel.Name.Should().Be(group.Name);
            viewModel.NameLower.Should().Be(group.Name.ToLower());
            viewModel.TotalAmount.Should().Be(group.TotalAmount.ToCurrencyString());
            viewModel.BudgetItems.Count.Should().Be(items.Count);

            for (int i = 0; i < items.Count; i++)
            {
                viewModel.BudgetItems[i].BudgetItem.Should().Be(items[i]);
            }
        }

        //[Fact]
        // public void Constructor_PopulatedBudgetPresentation_PresentPopulatedPresentationValues()
        // {
        //     var id = "group_id";
        //     var name = "test group";
        //     var colorKey = "test color key";
        //     var iconSource = "some uri";
        //     var description = "test description";
        //     var subtitle = "test subtitle";
        //     var addItemText = "add item test";
        //     var customTitle = "custom title";
        //     var presentation = new BudgetGroupPresentation(colorKey, iconSource, description, subtitle, addItemText,
        //         customTitle, true, true);
        //     var group = new BudgetGroup(id, name);
        //
        //     var viewModel = new BudgetGroupViewModel(group);
        //     
        //     viewModel.ColorKey.Should().Be(colorKey);
        //     viewModel.IconSource.Should().Be(iconSource);
        //     viewModel.Description.Should().Be(description);
        //     viewModel.Subtitle.Should().Be(subtitle);
        //     viewModel.AddItemText.Should().Be(addItemText);
        //     viewModel.CustomTitle.Should().Be(customTitle);
        //     viewModel.IsTitleVisible.Should().Be(false, "a custom title is present");
        //     viewModel.HasSecondaryHeader.Should().BeTrue();
        //     viewModel.HeaderText.Should().Be(viewModel.HeaderTextWithDate);
        // }

        [Fact]
        public void BudgetItems_DeleteItemGroup_ViewModelDeleted()
        {
            var items = new List<BudgetItem>
            {
                new BudgetItem("id1", "name1"),
                new BudgetItem("id2", "name2", 12.34m, BudgetItemType.Debt),
            };

            var group = new BudgetGroup("id", "group name", budgetItems: items);
            var budgetGroupViewModel = new BudgetGroupViewModel(group);

            var itemDeleted = group.BudgetItems[0];
            group.BudgetItems.RemoveAt(0);
            budgetGroupViewModel.BudgetItems.Count.Should().Be(1);
            budgetGroupViewModel.BudgetItems.Any(item => item.BudgetItem == itemDeleted).Should().BeFalse();
        }

        [Fact]
        public void IsTotalAmountZero_AllZeroItemsAmount_ExpectTrue()
        {
            var items = new List<BudgetItem>
            {
                new BudgetItem("1", "test 1"),
                new BudgetItem("2", "test 2"),
                new BudgetItem("3", "test 3")
            };

            var group = new BudgetGroup("1", "name", budgetItems: items);
            var viewModel = new BudgetGroupViewModel(group);

            viewModel.IsTotalAmountZero.Should().BeTrue();
        }

        [Fact]
        public void IsTotalAmountZero_NonZeroItemAmounts_ExpectFalse()
        {
            var items = new List<BudgetItem>
            {
                new BudgetItem("1", "test 1"),
                new BudgetItem("2", "test 2", 100m),
                new BudgetItem("3", "test 3")
            };
            
            var group = new BudgetGroup("1", "name", budgetItems: items);
            var viewModel = new BudgetGroupViewModel(group);

            viewModel.IsTotalAmountZero.Should().BeFalse();
        }

        [Fact]
        public void IsTotalAmountZero_ChangingItemAmounts_ExpectChangingTrueFalse()
        {
            var items = new List<BudgetItem>
            {
                new BudgetItem("1", "item 1"),
                new BudgetItem("2", "item 2"),
                new BudgetItem("3", "item 3"),
            };
            var group = new BudgetGroup("1", "group 1", budgetItems: items);
            var viewModel = new BudgetGroupViewModel(group);

            viewModel.IsTotalAmountZero.Should().BeTrue();
            group.BudgetItems[1].Amount = 234.56m;
            viewModel.IsTotalAmountZero.Should().BeFalse();
            group.BudgetItems[1].Amount = 0;
            viewModel.IsTotalAmountZero.Should().BeTrue();

        }

        [Fact]
        public void TotalAmount_ChangingItemAmounts_ExpectTotalAmountChangedEvent()
        {
            var items = new List<BudgetItem>
            {
                new BudgetItem("1", "item 1"),
                new BudgetItem("2", "item 2"),
                new BudgetItem("3", "item 3"),
            };

            var group = new BudgetGroup("1", "group 1", budgetItems: items);
            var viewModel = new BudgetGroupViewModel(group);
            bool wasChanged = false;
            viewModel.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(viewModel.TotalAmount))
                {
                    wasChanged = true;
                }
            };

            group.BudgetItems[1].Amount = 234.56m;
            wasChanged.Should().BeTrue();
            viewModel.TotalAmount.Should().Be(group.TotalAmount.ToCurrencyString());
        }
    }
    
}