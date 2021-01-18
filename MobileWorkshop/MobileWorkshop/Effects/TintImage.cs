using Xamarin.Forms;

namespace MobileWorkshop.Effects
{
    public class TintImage : RoutingEffect
    {
        public Color TintColor { get; private set; }
        public TintImage(Color color) : base($"OnboardingEffects.{nameof(TintImage)}")
        {
            TintColor = color;
        }
    }
}