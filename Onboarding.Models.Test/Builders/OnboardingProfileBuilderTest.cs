using FluentAssertions;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.Models.Test.Builders
{
    public class OnboardingProfileBuilderTest
    {
        [Fact]
        public void Build_ExpectPopulatedProfile()
        {
            var profile = OnboardingProfileBuilder.Build();

            profile.Should().NotBeNull();
            profile.Budget.Should().NotBeNull();
            profile.Goals.Should().NotBeNull();
            profile.Status.Should().NotBeNull();
        } 
    }
}