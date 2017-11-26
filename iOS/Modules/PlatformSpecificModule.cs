using Autofac;
using MicrosoftNews.Interfaces;
using MicrosoftNews.iOS.Services;

namespace MicrosoftNews.iOS.Modules
{
    public class PlatformSpecificModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<SqLitePathIos>().As<ISqLitePath>();
        }
    }
}
