using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("Onboarding.RemoteBudget.Test")]
namespace Onboarding.RemoteBudget
{
    public class BudgetItem
    {
        public readonly string Id;
        public readonly string Label;
        public readonly BudgetItemType Type;
        [JsonProperty("amount_budgeted")]
        public readonly int AmountBudgeted;

        [JsonConstructor]
        public BudgetItem(string id, BudgetItemType type, string label, int amountBudgeted)
        {
            Id = id;
            Type = type;
            Label = label;
            AmountBudgeted = amountBudgeted;
        }
    }
}