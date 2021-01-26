using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
[assembly: InternalsVisibleTo("Onboarding.Models.Test")]
[assembly: InternalsVisibleTo("Onboarding.RemoteBudget.Test")]
namespace Onboarding.RemoteBudget
{
    public class RemoteBudgetGroup
    {
        public readonly string Id;
        public readonly string Label;
        [JsonProperty("budget_items")]
        public readonly List<RemoteBudgetItem> BudgetItems;

        [JsonConstructor]
        public RemoteBudgetGroup(string id, string label, List<RemoteBudgetItem> budgetItems)
        {
            Id = id;
            Label = label;
            BudgetItems = budgetItems;
        }

    }
}