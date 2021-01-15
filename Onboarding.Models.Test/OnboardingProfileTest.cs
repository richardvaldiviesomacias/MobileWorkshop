using System;
using FluentAssertions;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.Models.Test
{
    public class OnboardingProfileTest
    {
        [Fact]
        public void Constructor_ExpectPopulatedParameters()
        {
            var budget = BudgetBuilder.Build();
            var goals = new TitledIconList();
            var status = new TitledIconList();

            var profile = new OnboardingProfile(budget, goals, status);

            profile.Budget.Should().NotBeNull();
            profile.Goals.Should().NotBeNull();
            profile.Status.Should().NotBeNull();
        }

        [Fact]
        public void Constructor_NullElements_ExpectException()
        {
            var budget = BudgetBuilder.Build();
            var goals = new TitledIconList();
            var status = new TitledIconList();

            Action testAction = () => new OnboardingProfile(null, goals, status);
            testAction.Should().Throw<ArgumentNullException>().WithMessage("*budget*");
        }

        [Fact]
        public void Constructor_NullGoals_ExpectException()
        {
            var budget = BudgetBuilder.Build();
            var status = new TitledIconList();

            Action testAction = () => new OnboardingProfile(budget, null, status);
            testAction.Should().Throw<ArgumentNullException>().WithMessage("*goals*");
        }

        [Fact]
        public void Constructor_NullStatus_ExpectException()
        {
            var budget = BudgetBuilder.Build();
            var goals = new TitledIconList();

            Action testAction = () => new OnboardingProfile(budget, goals, null);
            testAction.Should().Throw<ArgumentNullException>().WithMessage("*status*");
        }
    }
}