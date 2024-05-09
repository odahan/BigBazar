using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;

namespace BigBazar
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
      /*  public MainActivity()
        {
            var  s = GetType().FullName;
            Console.WriteLine(s);
        }*/
    }
}
