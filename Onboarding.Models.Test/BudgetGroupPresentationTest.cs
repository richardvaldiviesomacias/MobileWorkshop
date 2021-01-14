using FluentAssertions;
using Xunit;

namespace Onboarding.Models.Test
{
    public class BudgetGroupPresentationTest
    {
        [Fact]
        public void Constructor_DefaultValues_ExpectAssigment()
        {
            var presentation = new BudgetGroupPresentation();
            presentation.ColorKey.Should().Be(BudgetGroupPresentation.DefaultColorKey);
            presentation.IconSource.Should().Be("");
            presentation.Description.Should().Be("");
            presentation.Subtitle.Should().BeEquivalentTo(BudgetGroupPresentation.DefaultSubtitle);
            presentation.AddItemText.Should().Be(BudgetGroupPresentation.DefaultAddItemText);
            presentation.CustomTitle.Should().Be("");
            presentation.HasSecondaryHeader.Should().Be(false);
            presentation.HasDateOnHeader.Should().Be(false);
        }
    }
}