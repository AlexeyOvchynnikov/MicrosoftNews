using Android.App;
using Android.Content.PM;
using Android.OS;
using Autofac.Core;
using MicrosoftNews.Droid.Modules;

namespace MicrosoftNews.Droid
{
    [Activity(Label = "MicrosoftNews.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App(new IModule[] { new PlatformSpecificModule() }));
        }
    }
}
