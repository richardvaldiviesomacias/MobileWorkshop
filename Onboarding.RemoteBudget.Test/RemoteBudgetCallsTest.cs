using System.Linq;
using System.Threading.Tasks;
using AccessControl;
using FluentAssertions;
using RestSharp;
using Xunit;

namespace Onboarding.RemoteBudget.Test
{
    public class RemoteBudgetCallsTest
    {
        private readonly IAccessControlManager accessControlManager;
        private readonly IRestClient restClient;

        public RemoteBudgetCallsTest()
        {
            //signs in for every test
            accessControlManager = RemoteTesting.SignInForTesting();
            restClient = new RestClient("https://api.everydollar.com");
            
            //delete existing budget
            var remoteBudgetCalls = new RemoteBudgetCalls(accessControlManager, restClient);
            var task = Task.Run(async () => await remoteBudgetCalls.DeleteAllBudgets());
            task.Wait();
        }

        [Fact]
        public async void CreateNewBudget_ExpectSuccess()
        {
            var remoteBudgetCalls = new RemoteBudgetCalls(accessControlManager, restClient);
            await remoteBudgetCalls.CreateNewBudget();

            var budget = await remoteBudgetCalls.GetCurrentBudget();
            budget.Should().NotBeNull();
            budget.BudgetGroups.Should().NotBeNull();
            budget.BudgetGroups[0].BudgetItems.Should().NotBeNull();
        }

        [Fact]
        public async void GetBudgets_ExpectSuccess()
        {
            var remoteBudgetCalls = new RemoteBudgetCalls(accessControlManager, restClient);
            await remoteBudgetCalls.CreateNewBudget();
            var budgets = await remoteBudgetCalls.GetAllBudgets();
            budgets.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateBudgetItem_ExpectSuccess()
        {
            var remoteBudgetCalls = new RemoteBudgetCalls(accessControlManager, restClient);
            await remoteBudgetCalls.CreateNewBudget();
            
            var budget = await remoteBudgetCalls.GetCurrentBudget();
            decimal amount = 234.56m;
            await remoteBudgetCalls.UpdateBudgetItem(budget.Id, budget.BudgetGroups[0].BudgetItems[0].Id, amount);

            var updatedBudget = await remoteBudgetCalls.GetCurrentBudget();
            updatedBudget.BudgetGroups[0].BudgetItems[0].AmountBudgeted.Should().Be((int)(amount * 100));
        }

        [Fact]
        public async Task CreateBudgetItem_ExpectSuccess()
        {
            var remoteBudgetCalls = new RemoteBudgetCalls(accessControlManager, restClient);
            await remoteBudgetCalls.CreateNewBudget();

            var budget = await remoteBudgetCalls.GetCurrentBudget();
            
            decimal amount = 234.56m;
            string label = "totally unit testing";
            BudgetItemType type = BudgetItemType.Income;
            await remoteBudgetCalls.CreateBudgetItem(budget.Id, budget.BudgetGroups[0].Id, type, label, amount);

            var updateBudget = await remoteBudgetCalls.GetCurrentBudget();
            var addedItem = updateBudget.BudgetGroups[0].BudgetItems.Where(i => i.Label == label).FirstOrDefault();

            addedItem.Should().NotBeNull();
            addedItem.Type.Should().Be(type);
            addedItem.AmountBudgeted.Should().Be((int) (amount * 100));
        }

        [Fact]
        public async Task DeleteBudgetItem_ExpectSuccess()
        {
            var remoteBudgetCalls = new RemoteBudgetCalls(accessControlManager, restClient);
            await remoteBudgetCalls.CreateNewBudget();
            var budget = await remoteBudgetCalls.GetCurrentBudget();

            var itemId = budget.BudgetGroups[0].BudgetItems[0].Id;
            await remoteBudgetCalls.DeleteBudgetItem(budget.Id, itemId);
            
            budget = await remoteBudgetCalls.GetCurrentBudget();
            budget.BudgetGroups[0].BudgetItems.Any(i => i.Id == itemId).Should().BeFalse();
        }
        
    }
}