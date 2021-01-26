using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
[assembly: InternalsVisibleTo("Onboarding.Models.Test")]
[assembly: InternalsVisibleTo("Onboarding.RemoteBudget.Test")]
namespace Onboarding.RemoteBudget
{
    public class RemoteBudget
    {
        public readonly string Id;
        [JsonProperty("budget_groups")] public readonly List<RemoteBudgetGroup> BudgetGroups;

        [JsonConstructor]
        public RemoteBudget(string id, List<RemoteBudgetGroup> budgetGroups)
        {
            Id = id;
            BudgetGroups = budgetGroups;
        }
    }
}