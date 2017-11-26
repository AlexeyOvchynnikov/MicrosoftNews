using Autofac.Core;
using Foundation;
using MicrosoftNews.iOS.Modules;
using UIKit;

namespace MicrosoftNews.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App(new IModule[] { new PlatformSpecificModule() }));

            return base.FinishedLaunching(app, options);
        }
    }
}
