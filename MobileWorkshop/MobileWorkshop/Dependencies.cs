using Microsoft.Extensions.DependencyInjection;
using Onboarding.Models;
using Onboarding.Models.Builders;
using Onboarding.ViewModels;

namespace MobileWorkshop
{
    public static class Dependencies
    {
        static ServiceCollection collection;
        public static ServiceProvider ServiceProvider { get; private set; }

        // Helpers for easy access
        public static OnboardingProfile Profile => ServiceProvider.GetService<OnboardingProfile>();
        public static OnboardingProfileViewModel ProfileViewModel => ServiceProvider.GetService<OnboardingProfileViewModel>();


        public static void Init()
        {
            collection = new ServiceCollection();
            RegisterServices(collection);
            ServiceProvider = collection.BuildServiceProvider();
        }

        static void RegisterServices(IServiceCollection collection)
        {
            var budget = BudgetBuilder.Build();
            var profile = OnboardingProfileBuilder.Build(budget);

            // Models
            collection.AddSingleton(budget);
            collection.AddSingleton(profile);

            // View models
            collection.AddSingleton<BudgetViewModel>();
            collection.AddSingleton<OnboardingProfileViewModel>();
        }
    }
}