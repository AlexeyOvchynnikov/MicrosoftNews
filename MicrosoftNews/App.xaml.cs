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
        private static Repository<NewsModel> _newsRepository;
        private static Repository<TimeStampModel> _timeStampRepository;

        public static Repository<NewsModel> NewsRepository => _newsRepository ?? (_newsRepository = _container.Resolve<Repository<NewsModel>>());
        public static Repository<TimeStampModel> TimeStampRepository => _timeStampRepository ?? (_timeStampRepository = _container.Resolve<Repository<TimeStampModel>>());

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

            containerBuilder.RegisterType<Repository<NewsModel>>().SingleInstance();
            containerBuilder.RegisterType<Repository<TimeStampModel>>().SingleInstance();

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
