using System;
using System.Collections.Generic;
using System.Linq;
using Onboarding.Models;

namespace Onboarding.ViewModels
{
    public class OnboardingProfileViewModel
    {
        readonly List<TitledIconViewModel> goals = new List<TitledIconViewModel>();
        readonly List<TitledIconViewModel> status = new List<TitledIconViewModel>();

        internal readonly OnboardingProfile Profile;

        public BudgetViewModel Budget { get; private set; }
        public List<TitledIconViewModel> Goals => goals;  // Because Xamarin.Forms cannot bind to readonly, we expose a getter to our collection
        public List<TitledIconViewModel> Status => status;  // Because Xamarin.Forms cannot bind to readonly, we expose a getter to our collection

        public OnboardingProfileViewModel(OnboardingProfile profile, BudgetViewModel budgetViewModel)
        {
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
            Budget = budgetViewModel ?? throw new ArgumentNullException(nameof(budgetViewModel));

            // Create VMs for all goal, status, and step items
            profile.Goals.ToList().ForEach(goal => Goals.Add(new TitledIconViewModel(goal)));
            profile.Status.ToList().ForEach(status => Status.Add(new TitledIconViewModel(status)));
        }

    }
}