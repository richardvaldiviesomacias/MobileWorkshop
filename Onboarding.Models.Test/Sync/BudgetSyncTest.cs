using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Onboarding.Models.Builders;
using Onboarding.Models.Sync;
using Onboarding.RemoteBudget;
using Xunit;

namespace Onboarding.Models.Test.Sync
{
    public class BudgetSyncTest
    {
        [Fact]
        public void Constructor_ValidArguments_ExpectPopulatedParameters()
        {
            var remoteBudgetCalls = Substitute.For<IRemoteBudgetCalls>();
            var budgetGroupSync = new BudgetGroupSync(remoteBudgetCalls);
            var profileSync = new BudgetSync(remoteBudgetCalls, budgetGroupSync);
            profileSync.Should().NotBeNull();
        }

        [Fact]
        public void Constructor_NullRemoteBudget_ExpectException()
        {
            var remoteBudgetCalls = Substitute.For<IRemoteBudgetCalls>();
            Action testAction = () => new BudgetSync(null, new BudgetGroupSync(remoteBudgetCalls));
            testAction.Should().Throw<ArgumentNullException>().WithMessage("*remoteBudgetCalls*");
        }

        [Fact]
        public void Constructor_NullBudgetGroupSync_ExpectAHeckNo()
        {
            var remoteBudgetCalls = Substitute.For<IRemoteBudgetCalls>();
            Action testAction = () => new BudgetSync(remoteBudgetCalls, null);
            testAction.Should().Throw<ArgumentNullException>().WithMessage("*budgetGroupSync*");
        }

        [Fact]
        public void RemoteSyncBudget_NullBudget_ExpectException()
        {
            var remoteBudgetCalls = Substitute.For<IRemoteBudgetCalls>();
            var budgetGroupSync = Substitute.ForPartsOf<BudgetGroupSync>(remoteBudgetCalls);
            var sync = new BudgetSync(remoteBudgetCalls, budgetGroupSync);

            Func<Task> testAction = async () => await sync.RemoteSyncBudget(null);
            testAction.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public async Task RemoteSyncBudget_ExpectAllBudgetGroupsSyncs()
        {
            var remoteBudgetCalls = Substitute.For<IRemoteBudgetCalls>();
            //????
            var budgetGroupSync = Substitute.ForPartsOf<BudgetGroupSync>(remoteBudgetCalls);
            //????
            var sync = new BudgetSync(remoteBudgetCalls, budgetGroupSync);
            Budget budget = BudgetBuilder.Build();
            var testBudget = CreateTestRemoteBudget();
            remoteBudgetCalls.GetCurrentBudget().ReturnsForAnyArgs(testBudget);

            
            await sync.RemoteSyncBudget(budget);

           
            await budgetGroupSync.Received().SyncBudgetGroup(testBudget, budget.IncomeGroup);
            await budgetGroupSync.Received().SyncBudgetGroup(testBudget, budget.HousingGroup);
            await budgetGroupSync.Received().SyncBudgetGroup(testBudget, budget.TransportationGroup);
            await budgetGroupSync.Received().SyncBudgetGroup(testBudget, budget.FoodGroup);
            await budgetGroupSync.Received().SyncBudgetGroup(testBudget, budget.PersonalGroup);
            await budgetGroupSync.Received().SyncBudgetGroup(testBudget, budget.GivingGroup);
            await budgetGroupSync.Received().SyncBudgetGroup(testBudget, budget.DebtGroup);

            
            await budgetGroupSync.Received(7).SyncBudgetGroup(Arg.Any<RemoteBudget.Budget>(), Arg.Any<BudgetGroup>());
        }

        [Fact]
        public async Task RemoteSyncBudget_SyncStatus_ExpectChanges()
        {
            var remoteBudgetCalls = Substitute.For<IRemoteBudgetCalls>();
            var budgetGroupSync = Substitute.ForPartsOf<BudgetGroupSync>(remoteBudgetCalls);
            var budgetSync = new BudgetSync(remoteBudgetCalls, budgetGroupSync);
            Budget budget = BudgetBuilder.Build();

            var testBudget = CreateTestRemoteBudget();
            remoteBudgetCalls.GetCurrentBudget().ReturnsForAnyArgs(testBudget);

            bool statusChanged = false;
            string status = "";
            budgetSync.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(budgetSync.SyncingStatus))
                {
                    statusChanged = true;
                    status = (sender as BudgetSync).SyncingStatus;
                }
            };

            await budgetSync.RemoteSyncBudget(budget);
            statusChanged.Should().BeTrue();
            status.Should().NotBeNullOrWhiteSpace();
        }
        
        public static RemoteBudget.Budget CreateTestRemoteBudget()
        {
            return new RemoteBudget.Budget("test", new List<RemoteBudget.BudgetGroup>()
            {
                new RemoteBudget.BudgetGroup("Income", "Income", new List<RemoteBudget.BudgetItem>()
                {
                    new RemoteBudget.BudgetItem("test", BudgetItemType.Income, "Paycheck 1", 0),
                    new RemoteBudget.BudgetItem("test", BudgetItemType.Income, "Paycheck 2", 0),
                }),
                new RemoteBudget.BudgetGroup("Housing", "Housing", new List<RemoteBudget.BudgetItem>(){

                    new RemoteBudget.BudgetItem("test", BudgetItemType.Expense, "Mortgage/Rent", 0),
                    new RemoteBudget.BudgetItem("test", BudgetItemType.Expense, "Water", 0),
                    new RemoteBudget.BudgetItem("test", BudgetItemType.Expense, "Natural Gas", 0),
                    new RemoteBudget.BudgetItem("test", BudgetItemType.Expense, "Electricity", 0),
                    new RemoteBudget.BudgetItem("test", BudgetItemType.Expense, "Cable", 0),
                    new RemoteBudget.BudgetItem("test", BudgetItemType.Expense, "Trash", 0),
                }),
                new RemoteBudget.BudgetGroup("Transportation", "Transportation", null),
                new RemoteBudget.BudgetGroup("Food", "Food", null),
                new RemoteBudget.BudgetGroup("Personal", "Personal", null),
                new RemoteBudget.BudgetGroup("Giving", "Giving", null),
                new RemoteBudget.BudgetGroup("Debt", "Debt", null),
            });
        }
    }
}