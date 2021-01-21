using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Onboarding.RemoteBudget;

namespace Onboarding.Models.Sync
{
    public class BudgetGroupSync
    {
        private readonly IRemoteBudgetCalls remoteBudgetCalls;
        
        public BudgetGroupSync(IRemoteBudgetCalls remoteBudgetCalls)
        {
            this.remoteBudgetCalls = remoteBudgetCalls ?? throw new ArgumentNullException(nameof(remoteBudgetCalls));
        }

        public virtual async Task SyncBudgetGroup(RemoteBudget.Budget remoteBudget, BudgetGroup budgetGroup)
        {
            var budgetId = remoteBudget?.Id ?? throw new ArgumentNullException(nameof(remoteBudget));
            RemoteBudget.BudgetGroup remoteGroup = remoteBudget?.BudgetGroups
                ?.Where(g => g.Label.ToLower() == budgetGroup.Name.ToLower())
                .FirstOrDefault() ?? throw new Exception($"Budget group not found: {budgetGroup.Name}");

            var taskList = new List<Task>();
            budgetGroup.BudgetItems.ToList().ForEach(item => taskList.Add(AddOrUpdateBudgetItem(budgetId, item, remoteGroup)));
            remoteGroup.BudgetItems?.ToList().ForEach(
                remoteItem => taskList.Add(RemoveUnusedBudgetItems(budgetId, remoteItem, budgetGroup)));
            
            await Task.WhenAll(taskList);
        }

        private async Task AddOrUpdateBudgetItem(string budgetId, BudgetItem item, RemoteBudget.BudgetGroup remoteGroup)
        {
            if (item.Amount == 0m)
            {
                return;
            }

            var remoteItem = remoteGroup.BudgetItems?.Where(i => i.Label == item.Name).FirstOrDefault();
            if (remoteItem == null)
            {
                await remoteBudgetCalls.CreateBudgetItem(budgetId, remoteGroup.Id, item.Type, item.Name, item.Amount);
                return;
            }
            await remoteBudgetCalls.UpdateBudgetItem(budgetId, remoteItem.Id, item.Amount);
        }
        public async Task RemoveUnusedBudgetItems(string budgetId, RemoteBudget.BudgetItem remoteItem, BudgetGroup profileGroup)
        {
            var profileItem = profileGroup.BudgetItems.Where(i => i.Name == remoteItem.Label).FirstOrDefault();

            // Not in the local profile? DELETE!
            if (!IsRemoteBudgetItemUsedInProfile(profileGroup, remoteItem))
            {
                await remoteBudgetCalls.DeleteBudgetItem(budgetId, remoteItem.Id);
            }
        }
        
        internal bool IsRemoteBudgetItemUsedInProfile(BudgetGroup profileGroup, RemoteBudget.BudgetItem remoteItem)
        {
            // Search for matching items in the profile
            var profileItem = profileGroup.BudgetItems.Where(i => i.Name == remoteItem.Label).FirstOrDefault();

            // Only count populated budget items with non-zero values as having been used
            return profileItem != null && profileItem.Amount != 0m;
        }
    }
}