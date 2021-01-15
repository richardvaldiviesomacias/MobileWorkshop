using System;
using FluentAssertions;
using Onboarding.Models;
using Xunit;

namespace Onboarding.ViewModels.Test
{
    public class TitledIconViewModelTest
    {
        [Fact]
        public void Constructor_NullModel_ExpectKaboom()
        {
            Action testAction = () => new TitledIconViewModel(null);
            testAction.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_ValidModel_ExpectPresentation()
        {
            var titledIcon = new TitledIcon("id", "title", "image");
            titledIcon.IsSelected = true;
            var viewModel = new TitledIconViewModel(titledIcon);

            viewModel.Title.Should().Be(titledIcon.Title);
            viewModel.ImageSource.Should().Be(titledIcon.ImageSource);
            viewModel.IsSelected.Should().Be(titledIcon.IsSelected);
        }

        [Fact]
        public void IsNotSelected_DependsOnIsSelectedChanged_ExpectNotification()
        {
            var titledIcon = new TitledIcon("id", "title", "image");
            var viewModel = new TitledIconViewModel(titledIcon);
            bool wasNotified = false;
            viewModel.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(viewModel.IsNotSelected)) wasNotified = true;
            };

            // TEST: change model isSelected
            viewModel.IsSelected = true;

            titledIcon.IsSelected.Should().BeTrue();
            viewModel.IsNotSelected.Should().BeFalse();
            wasNotified.Should().BeTrue();
        }
    }
}