using System;
using FluentAssertions;
using Onboarding.Models.Builders;
using Onboarding.Models.Sync;
using Xunit;

namespace Onboarding.Models.Test.Sync
{
    public class GoalsStatusSyncTest
    {
        [Fact]
        public void LogSelectedStatus_ValidStatus_ExpectLogged()
        {
            var sync = new GoalsStatusSync();
            var status = OnboardingProfileBuilder.Build().Status;
            sync.LogSelectedStatus(status);
        }

        [Fact]
        public void LogSelectedStatus_Null_ExpectException()
        {
            var sync = new GoalsStatusSync();
            Action testAction = () => sync.LogSelectedStatus(null);
            testAction.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void LogSelectedGoals_Null_ExpectException()
        {
            var sync = new GoalsStatusSync();
            Action testAction = () => sync.LogSelectedGoals(null);

            testAction.Should().Throw<ArgumentNullException>();
        }
    }
}