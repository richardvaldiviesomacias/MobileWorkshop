using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("Onboarding.RemoteBudget.Test")]
namespace Onboarding.RemoteBudget
{
    public class BudgetGroup
    {
        public readonly string Id;
        public readonly string Label;
        [JsonProperty("budget_items")]
        public readonly List<BudgetItem> BudgetItems;

        [JsonConstructor]
        public BudgetGroup(string id, string label, List<BudgetItem> budgetItems)
        {
            Id = id;
            Label = label;
            BudgetItems = budgetItems;
        }

    }
}