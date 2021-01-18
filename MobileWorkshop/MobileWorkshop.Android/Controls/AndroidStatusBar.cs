using System;
using Android.Views;
using MobileWorkshop.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace MobileWorkshop.Android.Controls
{
    public class AndroidStatusBar : IStatusBar
    {
        public void SetColor(Color color)
        {
            var window = MainActivity.CurrentActivity.Window;

            window.SetStatusBarColor(color.ToAndroid());

            // For light colors, switch status bar icons to dark
            // BTW, Color.Default has a -1 luminosity.   Which is hard to deal with, since it should be 0 to 1. So we do a Math.Abs here
            if (Math.Abs(color.Luminosity) > 0.6)
            {
                window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            }
        }
    }
}