using System;
using MobileWorkshop.Onboarding;
using Onboarding.RemoteBudget;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace MobileWorkshop
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Dependencies.Init(RemoteTesting.SignInForTesting());
            MainPage = new NavigationPage(new IntroPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}