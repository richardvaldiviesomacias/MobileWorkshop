using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Onboarding.Models.Builders;
using Onboarding.Models.Sync;
using Onboarding.RemoteBudget;
using Xunit;

namespace Onboarding.Models.Test.Sync
{
    public class BudgetGroupSyncTest
    {

        [Fact]
        public void Constructor_ValidArguments_ExpectPopulatedParameters()
        {
            var remoteBudgetCalls = Substitute.For<IRemoteBudgetCalls>();
            var budgetGroupSync = new BudgetGroupSync(remoteBudgetCalls);
            budgetGroupSync.Should().NotBeNull();
        }

        [Fact]
        public void Constructor_NullRemoteBudgetCalls_ExpectException()
        {
            Action testAction = () => new BudgetGroupSync(null);
            testAction.Should().Throw<ArgumentNullException>().WithMessage($"*remoteBudgetCalls*");
        }

        [Fact]
        public async Task SyncBudgetGroup_UpdateBudgetItem_ExpectRemoteItemUpdate()
        {
            var remoteBudgetCalls = Substitute.For<IRemoteBudgetCalls>();
            var budgetGroupSync = new BudgetGroupSync(remoteBudgetCalls);
            var remoteBudget = CreateTestRemoteBudget();
            var budget = BudgetBuilder.Build();
            decimal amount = 123.45m;
            budget.IncomeGroup.BudgetItems[0].Amount = amount;

            await budgetGroupSync.SyncBudgetGroup(remoteBudget, budget.IncomeGroup);
            await remoteBudgetCalls.Received()
                .UpdateBudgetItem(remoteBudget.Id, remoteBudget.BudgetGroups[0].BudgetItems[0].Id, amount);
            
        }

        [Fact]
        public async Task SyncBudgetGroup_DeleteBudgetItem_ExpectRemoteItemDelete()
        {
            var remoteBudgetCalls = Substitute.For<IRemoteBudgetCalls>();
            var budgetGroupSync = new BudgetGroupSync(remoteBudgetCalls);
            var remoteBudget = CreateTestRemoteBudget();
            var budget = BudgetBuilder.Build();
            budget.IncomeGroup.BudgetItems.RemoveAt(0);

            await budgetGroupSync.SyncBudgetGroup(remoteBudget, budget.IncomeGroup);
            await remoteBudgetCalls.Received()
                .DeleteBudgetItem(remoteBudget.Id, remoteBudget.BudgetGroups[0].BudgetItems[0].Id);
        }


        [Fact]
        public async Task SyncBudgetGroup_AddBudgetItem_ExpectRemoteAdd()
        {
            var remoteBudgetCalls = Substitute.For<IRemoteBudgetCalls>();
            var budgetGroupSync = new BudgetGroupSync(remoteBudgetCalls);
            var remoteBudget = CreateTestRemoteBudget();
            var budget = BudgetBuilder.Build();

            // Create an income group with 2 existing populated amounts and 1 new addition
            budget.IncomeGroup.BudgetItems[0].Amount = 10m;
            budget.IncomeGroup.BudgetItems[1].Amount = 10m;
            budget.IncomeGroup.AddNewBudgetItem();
            var newItem = budget.IncomeGroup.BudgetItems.Last();
            var index = budget.IncomeGroup.BudgetItems.IndexOf(newItem);
            budget.IncomeGroup.BudgetItems[index].Name = "test";
            budget.IncomeGroup.BudgetItems[index].Amount = 13m;

            await budgetGroupSync.SyncBudgetGroup(remoteBudget, budget.IncomeGroup);

            await remoteBudgetCalls.Received().CreateBudgetItem(remoteBudget.Id,remoteBudget.BudgetGroups[0].Id,
                newItem.Type,newItem.Name,
                newItem.Amount); ;
        }
        
        public static Onboarding.RemoteBudget.RemoteBudget CreateTestRemoteBudget()
        {
            return new RemoteBudget.RemoteBudget("test", new List<RemoteBudget.RemoteBudgetGroup>
            {
                new RemoteBudget.RemoteBudgetGroup("Income", "Income", new List<RemoteBudget.RemoteBudgetItem>
                {
                    new RemoteBudget.RemoteBudgetItem("test", RemoteBudget.BudgetItemType.Income, "Paycheck 1", 0),
                    new RemoteBudget.RemoteBudgetItem("test", RemoteBudget.BudgetItemType.Income, "Paycheck 2", 0),
                }),
                
                new RemoteBudget.RemoteBudgetGroup("Housing", "Housing", new List<RemoteBudget.RemoteBudgetItem>
                {
                    new RemoteBudget.RemoteBudgetItem("test", RemoteBudget.BudgetItemType.Expense, "Mortgage/Rent", 0),
                    new RemoteBudget.RemoteBudgetItem("test", RemoteBudget.BudgetItemType.Expense, "Water", 0),
                    new RemoteBudget.RemoteBudgetItem("test", RemoteBudget.BudgetItemType.Expense, "Natural Gas", 0),
                    new RemoteBudget.RemoteBudgetItem("test", RemoteBudget.BudgetItemType.Expense, "Electricity", 0),
                    new RemoteBudget.RemoteBudgetItem("test", RemoteBudget.BudgetItemType.Expense, "Cable", 0),
                    new RemoteBudget.RemoteBudgetItem("test", RemoteBudget.BudgetItemType.Expense, "Trash", 0),
                }),
                
                new RemoteBudget.RemoteBudgetGroup("Transportation", "Transportation", null),
                new RemoteBudget.RemoteBudgetGroup("Food", "Food", null),
                new RemoteBudget.RemoteBudgetGroup("Personal", "Personal", null),
                new RemoteBudget.RemoteBudgetGroup("Giving", "Giving", null),
                new RemoteBudget.RemoteBudgetGroup("Debt", "Debt", null),
            });
        }
    }
}