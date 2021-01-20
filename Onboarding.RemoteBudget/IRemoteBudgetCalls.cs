using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onboarding.RemoteBudget
{
    public interface IRemoteBudgetCalls
    {
        Task<string> SignIn(string email, string password);
        Task<Budget> CreateNewBudget();
        Task<Budget> GetCurrentBudget();
        Task<List<Budget>> GetAllBudgets();
        Task UpdateBudgetItem(string budgetId, string itemId, decimal amount);
        Task DeleteBudget(string budgetId);
        Task DeleteBudgetItem(string budgetId, string itemId);
        Task CreateBudgetItem(string budgetId, string groupId, BudgetItemType type, string label, decimal amount);
    }
}