using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
[assembly: InternalsVisibleTo("Onboarding.Models.Test")]
[assembly: InternalsVisibleTo("Onboarding.RemoteBudget.Test")]
namespace Onboarding.RemoteBudget
{
    public class Budget
    {
        public readonly string Id;
        [JsonProperty("budget_groups")] public readonly List<BudgetGroup> BudgetGroups;

        [JsonConstructor]
        public Budget(string id, List<BudgetGroup> budgetGroups)
        {
            Id = id;
            BudgetGroups = budgetGroups;
        }
    }
}