using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onboarding.RemoteBudget
{
    public interface IRemoteBudgetCalls
    {
        Task<string> SignIn(string email, string password);
        Task<RemoteBudget> CreateNewBudget();
        Task<RemoteBudget> GetCurrentBudget();
        Task<List<RemoteBudget>> GetAllBudgets();
        Task UpdateBudgetItem(string budgetId, string itemId, decimal amount);
        Task DeleteBudget(string budgetId);
        Task DeleteBudgetItem(string budgetId, string itemId);
        Task CreateBudgetItem(string budgetId, string groupId, BudgetItemType type, string label, decimal amount);
    }
}