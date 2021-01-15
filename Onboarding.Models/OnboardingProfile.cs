using System;

namespace Onboarding.Models
{
    public class OnboardingProfile
    {
        public readonly Budget Budget;
        public readonly TitledIconList Goals;
        public readonly TitledIconList Status;

        public OnboardingProfile(
            Budget budget,
            TitledIconList goals,
            TitledIconList status)
        {
            Budget = budget ?? throw new ArgumentNullException(nameof(budget));
            Goals = goals ?? throw new ArgumentNullException(nameof(goals));
            Status = status ?? throw new ArgumentNullException(nameof(status));
        }
    }
}