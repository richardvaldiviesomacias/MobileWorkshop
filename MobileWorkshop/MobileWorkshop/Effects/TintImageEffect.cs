using System.Linq;
using Xamarin.Forms;

namespace MobileWorkshop.Effects
{
    public static class TintImageEffect
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create("TintColor", typeof(Color), typeof(Color), propertyChanged: OnTintColorPropertyPropertyChanged);

        public static Color GetTintColor(BindableObject element)
        {
            return (Color)element.GetValue(TintColorProperty);
        }

        public static void SetTintColor(BindableObject element, Color value)
        {
            element.SetValue(TintColorProperty, value);
        }


        static void OnTintColorPropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable as Image, (Color)newValue);
        }

        public static void AttachEffect(Image element, Color color)
        {
            var effect = element.Effects.FirstOrDefault(x => x is TintImage) as TintImage;

            if (effect != null)
            {
                element.Effects.Remove(effect);
            }

            element.Effects.Add(new TintImage(color));
        }

    }
}