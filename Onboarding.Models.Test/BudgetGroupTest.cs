using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Onboarding.RemoteBudget;
using Xunit;

namespace Onboarding.Models.Test
{
    public class BudgetGroupTest
    {

        [Fact]
        public void Constructor_ValidateParameters_ExpectAssignment()
        {
            var budgetItems = new List<BudgetItem> {new BudgetItem("id","name", 100.3m, BudgetItemType.Income)};
            var presentation = new BudgetGroupPresentation();
            
            var budgetGroup = new BudgetGroup("id", "name", budgetItems: budgetItems);
            
            budgetGroup.Id.Should().Be("id");
            budgetGroup.Name.Should().Be("name");
            budgetGroup.BudgetItems.Should().BeEquivalentTo(budgetItems);
        }

        [Fact]
        public void CollectionOfBudgetItems_CalculateTotal_ExpectRightAmount()
        {

            var budgetItems = new List<BudgetItem>
            {
                new BudgetItem("income_1","paycheck1", 100, BudgetItemType.Income),
                new BudgetItem("income_2","paycheck2", 300, BudgetItemType.Income),
            };
            var budgetGroup = new BudgetGroup("id", "Income", budgetItems: budgetItems);
            
            budgetGroup.TotalAmount.Should().Be(400);
        }

        // [Fact]
        // public void Constructor_PassNullItems_ExpectBigExplosion()
        // {
        //     Action testAction = () => new BudgetGroup("id", "name",  budgetItems:null);
        //     testAction.Should().Throw<ArgumentNullException>().WithMessage("*" + nameof(BudgetGroup.BudgetItems) + "*");
        // }
        
        [Fact]
        public void TotalAmount_ItemAmountSet_ExpectListenerNotified()
        {
            var items = new List<BudgetItem>()
            {
                new BudgetItem("item_id", "item name", 12.34m, BudgetItemType.Income)
            };

            var group = new BudgetGroup("group_id", "test group", budgetItems: items);

            bool wasChanged = false;
            group.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(group.TotalAmount)) wasChanged = true;
            };

            var newAmount = 99.99m;
            items[0].Amount = newAmount;
            wasChanged.Should().BeTrue();
        }

        [Fact]
        public void TotalAmount_Items_ExpectCalculateTotalAmountCorrect()
        {
            var firstItemAmount = 20m;
            var secondItemAmount = 30m;
            var thirdItemAmount = 40m;
            var total = firstItemAmount + secondItemAmount + thirdItemAmount;
            var items = new List<BudgetItem>
            {
                new BudgetItem("id_1", "item_name1", amount: firstItemAmount),
                new BudgetItem("id_2", "item_name2"),
                new BudgetItem("id_3", "item_name3"),
            };
            
            var group = new BudgetGroup("id","test group", budgetItems: items);

            items[1].Amount = secondItemAmount;
            items[2].Amount = thirdItemAmount;
            
            group.TotalAmount.Should().Be(total);
        }

        [Fact]
        public void TotalAmount_AddNewBudgetItem_ExpectGroupListenerNotified()
        {
            var items = new List<BudgetItem> { new BudgetItem("id", "name", 10, BudgetItemType.Debt)};
            var group = new BudgetGroup("id", "groupname", budgetItems: items);

            bool wasGroupNotified = false;
            group.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(group.TotalAmount))
                {
                    wasGroupNotified = true;
                }
            };

            var newAmount = 100;
            group.BudgetItems.Last().Amount = newAmount;
            wasGroupNotified.Should().BeTrue();
        }
    }
    
}