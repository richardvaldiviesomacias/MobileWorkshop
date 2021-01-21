using FluentAssertions;
using Onboarding.RemoteBudget;
using Xunit;

namespace Onboarding.Models.Test
{
    public class BudgetItemTest
    {
        [Fact]
        public void Constructor_ValidateParameters_ExpectAssignment()
        {
            var item = new BudgetItem("id", "name", 200, BudgetItemType.Income);
            item.Id.Should().Be("id");
            item.Name.Should().Be("name");
            item.Amount.Should().Be(200);
            item.Type.Should().Be(BudgetItemType.Income);
        }
    }
}