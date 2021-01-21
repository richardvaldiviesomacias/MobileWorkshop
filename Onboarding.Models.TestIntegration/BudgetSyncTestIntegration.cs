using System.Threading.Tasks;
using Onboarding.Models.Sync;
using Onboarding.RemoteBudget;
using RestSharp;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Onboarding.Models.TestIntegration
{
    public class BudgetSyncTestIntegration
    {
        readonly BudgetGroupSync budgetGroupSync;
        private readonly RemoteBudgetCalls remoteBudgetCalls;


        public BudgetSyncTestIntegration()
        {
            var accessControlManager = RemoteTesting.SignInForTesting();
            var restClient = new RestClient($"https://api.everydollar.com/");
            remoteBudgetCalls = new RemoteBudgetCalls(accessControlManager, restClient);
            budgetGroupSync = new BudgetGroupSync(remoteBudgetCalls);
            
            //delete existing budgets
            var task = Task.Run(async () => await remoteBudgetCalls.DeleteAllBudgets());
            task.Wait();
        }
        
        [Fact]
        public void RemoteSync_ExistingBudget_ExpectSuccess()
        {
            //var budgetSync = new BudgetSync();
        }
    }
}