using System;
using AccessControl;
using Microsoft.Extensions.DependencyInjection;
using Onboarding.Models;
using Onboarding.Models.Builders;
using Onboarding.Models.Sync;
using Onboarding.RemoteBudget;
using Onboarding.ViewModels;
using RestSharp;

namespace MobileWorkshop
{
    public static class Dependencies
    {
        static ServiceCollection collection;
        public static ServiceProvider ServiceProvider { get; private set; }

        // Helpers for easy access
        public static OnboardingProfile Profile => ServiceProvider.GetService<OnboardingProfile>();
        public static OnboardingProfileViewModel ProfileViewModel => ServiceProvider.GetService<OnboardingProfileViewModel>();
        public static BudgetSummaryViewModel BudgetSummaryViewModel => ServiceProvider.GetService<BudgetSummaryViewModel>();


        public static void Init(IAccessControlManager accessControlManager)
        {
            if(accessControlManager == null)
            {
                throw new ArgumentNullException(nameof(accessControlManager));
            }

            collection = new ServiceCollection();
            RegisterServices(collection, accessControlManager);
            ServiceProvider = collection.BuildServiceProvider();
        }

        static void RegisterServices(IServiceCollection collection, IAccessControlManager accessControlManager)
        {
            
            //Services
            var restClient = new RestClient("https://api.everydollar.com");
            
            collection.AddSingleton(accessControlManager);
            collection.AddSingleton<IRestClient>(restClient);
            collection.AddSingleton<IRemoteBudgetCalls, RemoteBudgetCalls>();
            
            //Models
            var budget = BudgetBuilder.Build();
            var profile = OnboardingProfileBuilder.Build(budget);

            collection.AddSingleton<BudgetGroupSync>();
            collection.AddSingleton<BudgetSync>();
            collection.AddSingleton<GoalsStatusSync>();
            collection.AddSingleton(budget);
            collection.AddSingleton<BudgetRatio>();
            collection.AddSingleton<BudgetSummary>();
            collection.AddSingleton(profile);

            // View models
            collection.AddSingleton<BudgetViewModel>();
            collection.AddSingleton<BudgetSummaryViewModel>();
            collection.AddSingleton<OnboardingProfileViewModel>();
        }
    }
}