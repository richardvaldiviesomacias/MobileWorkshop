using System.Linq;
using FluentAssertions;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.Models.Test
{
    public class TitledIconListTest
    {
        [Fact]
        public void GetSelectedIconIds_NoneSelected_ExpectNoneSelections()
        {
            TitledIconList list = OnboardingProfileBuilder.Goals;

            var ids = list.GetSelectedIconIds();

            ids.Should().BeEmpty();
        }

        [Fact]
        public void GetSelectedIconIds_SeveralSelected_ExpectMatcheselection()
        {
            TitledIconList list = OnboardingProfileBuilder.Goals;
            list[0].IsSelected = true;
            list[2].IsSelected = true;
            var expectedIds = list.Where(icon => icon.IsSelected).Select(icon => icon.Id).ToList();

            var ids = list.GetSelectedIconIds();

            ids.Should().BeEquivalentTo(expectedIds);
        }

        [Fact]
        public void GetSelectedIconIdsAsCommaSeparatedList_NoneSelected_EmptyString()
        {
            TitledIconList list = OnboardingProfileBuilder.Goals;

            var output = list.GetSelectedIconIdsAsCommaSeparatedList();
            output.Should().BeEmpty();
        }


        [Fact]
        public void GetSelectedIconIdsAsCommaSeparatedList_SeveralSelected_IdsMatch()
        {
            TitledIconList list = OnboardingProfileBuilder.Goals;
            list[0].IsSelected = true;
            list[2].IsSelected = true;
            var expectedString = string.Join(",", list.GetSelectedIconIds());

            var output = list.GetSelectedIconIdsAsCommaSeparatedList();
            output.Should().Be(expectedString);
        }
    }
}