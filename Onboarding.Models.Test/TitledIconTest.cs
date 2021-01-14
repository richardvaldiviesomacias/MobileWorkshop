using FluentAssertions;
using Xunit;

namespace Onboarding.Models.Test
{
    public class TitledIconTest
    {
        [Fact]
        public void Constructor_ValidateParameters_ExpectAssignment()
        {
            var id = "new_id";
            var title = "title";
            var imageSource = "image_source";
            var titledIcon = new TitledIcon(id, title, imageSource);
            titledIcon.Id.Should().Be(id);
            titledIcon.Title.Should().Be(title);
            titledIcon.ImageSource.Should().Be(imageSource);
        }

        [Fact]
        public void IsSelected_ValueChange_ExpectPropertyChange()
        {
            var id = "new_id";
            var title = "title";
            var imageSource = "image_source";
            var titledIcon = new TitledIcon(id, title, imageSource);
            titledIcon.IsSelected.Should().BeFalse();

            var wasChanged = false;
            //simulate PropertyChange call
            titledIcon.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(titledIcon.IsSelected))
                {
                    wasChanged = true;
                }
            };
            
            titledIcon.IsSelected = true;
            wasChanged.Should().BeTrue();
        }
    }
}