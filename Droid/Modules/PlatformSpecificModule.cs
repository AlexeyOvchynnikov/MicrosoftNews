using Autofac;
using MicrosoftNews.Droid.Services;
using MicrosoftNews.Interfaces;

namespace MicrosoftNews.Droid.Modules
{
    public class PlatformSpecificModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<SqLitePathAndroid>().As<ISqLitePath>();
        }
    }
}
