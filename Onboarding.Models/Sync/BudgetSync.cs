using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Onboarding.RemoteBudget;

namespace Onboarding.Models.Sync
{
    public class BudgetSync: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        readonly IRemoteBudgetCalls remoteBudgetCalls;
        readonly BudgetGroupSync budgetGroupSync;
        public string SyncingStatus { get; private set; } = "";
        public BudgetSync(IRemoteBudgetCalls remoteBudgetCalls, BudgetGroupSync budgetGroupSync)
        {
            this.remoteBudgetCalls = remoteBudgetCalls ?? throw new ArgumentNullException(nameof(remoteBudgetCalls));
            this.budgetGroupSync = budgetGroupSync ?? throw new ArgumentNullException(nameof(budgetGroupSync));
        }

        public async Task RemoteSyncBudget(Budget budget)
        {
            if (budget == null)
            {
                throw new ArgumentNullException(nameof(budget));
            }

            SyncingStatus = "creating new budget";
            await remoteBudgetCalls.CreateNewBudget();
            var remoteBudget = await remoteBudgetCalls.GetCurrentBudget();
            
            SyncingStatus = "income";
            await budgetGroupSync.SyncBudgetGroup(remoteBudget, budget.IncomeGroup);
            SyncingStatus = "housing";
            await budgetGroupSync.SyncBudgetGroup(remoteBudget, budget.HousingGroup);
            SyncingStatus = "transportation";
            await budgetGroupSync.SyncBudgetGroup(remoteBudget, budget.TransportationGroup);
            SyncingStatus = "food";
            await budgetGroupSync.SyncBudgetGroup(remoteBudget, budget.FoodGroup);
            SyncingStatus = "personal";
            await budgetGroupSync.SyncBudgetGroup(remoteBudget, budget.PersonalGroup);
            SyncingStatus = "giving";
            await budgetGroupSync.SyncBudgetGroup(remoteBudget, budget.GivingGroup);
            SyncingStatus = "debt";
            await budgetGroupSync.SyncBudgetGroup(remoteBudget, budget.DebtGroup);

        }

    }
}