using Autofac;
using Autofac.Core;
using MicrosoftNews.Models;
using MicrosoftNews.Repositories;
using MicrosoftNews.ViewModels;
using MicrosoftNews.Views;
using Xamarin.Forms;

namespace MicrosoftNews
{
    public partial class App : Application
    {
        private static IContainer _container;
        private static Repository<News> _newsRepository;
        private static Repository<TimeStamp> _timeStampRepository;

        public static Repository<News> NewsRepository => _newsRepository ?? (_newsRepository = _container.Resolve<Repository<News>>());
        public static Repository<TimeStamp> TimeStampRepository => _timeStampRepository ?? (_timeStampRepository = _container.Resolve<Repository<TimeStamp>>());

        public App(IModule[] platformSpecificModules)
        {
            PrepareContainer(platformSpecificModules);

            InitializeComponent();
            var mainPage = new MainPage();
            mainPage.BindingContext = new MainViewModel(mainPage.Navigation);
            MainPage = new NavigationPage(mainPage);
        }

        private static void PrepareContainer(IModule[] platformSpecificModules)
        {
            var containerBuilder = new ContainerBuilder();
            RegisterPlatformSpecificModules(platformSpecificModules, containerBuilder);

            containerBuilder.RegisterType<Repository<News>>().SingleInstance();
            containerBuilder.RegisterType<Repository<TimeStamp>>().SingleInstance();

            _container = containerBuilder.Build();
        }

        private static void RegisterPlatformSpecificModules(IModule[] platformSpecificModules, ContainerBuilder containerBuilder)
        {
            foreach (var platformSpecificModule in platformSpecificModules)
            {
                containerBuilder.RegisterModule(platformSpecificModule);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
