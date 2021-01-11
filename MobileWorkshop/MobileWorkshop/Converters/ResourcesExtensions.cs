using Xamarin.Forms;

namespace MobileWorkshop.Converters
{
    public static class ResourcesExtensions
    {
        public static Color DefaultColor => Color.Maroon;

        public static object GetValue(this ResourceDictionary resources, string key)
        {
            // Search all dictionaries
            if (resources.TryGetValue(key, out var retVal))
            {
                return retVal;
            }

            return null;
        }

        public static Color GetColor(this ResourceDictionary resources, string key) =>
            resources.GetValue(key) as Color? ?? DefaultColor;
    }
}