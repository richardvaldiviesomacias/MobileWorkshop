using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Onboarding.Models;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.ViewModels.Test
{
    public class OnboardingProfileViewModelTest
    {
        private readonly Budget budget;
        private readonly BudgetViewModel budgetViewModel;
        private readonly OnboardingProfile profile;

        public OnboardingProfileViewModelTest()
        {
            budget = BudgetBuilder.Build();
            budgetViewModel = new BudgetViewModel(budget);
            profile = OnboardingProfileBuilder.Build(budget);
        }

        [Fact]
        public void Constructor_ValidModel_ExpectPresentation()
        {
            var expectedGoals = new List<TitledIconViewModel>();
            var expectedStatus = new List<TitledIconViewModel>();
            
            profile.Goals.ToList().ForEach(goal => expectedGoals.Add(new TitledIconViewModel(goal)));
            profile.Status.ToList().ForEach(status => expectedStatus.Add(new TitledIconViewModel(status)));
            
            var viewModel = new OnboardingProfileViewModel(profile, budgetViewModel);
            viewModel.Budget.Should().Be(budgetViewModel);
        }
    }
}