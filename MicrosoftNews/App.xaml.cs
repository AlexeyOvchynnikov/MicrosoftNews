using Autofac;
using Autofac.Core;
using MicrosoftNews.Models;
using MicrosoftNews.Repositories;
using MicrosoftNews.Repositories.Interfaces;
using MicrosoftNews.ViewModels;
using MicrosoftNews.Views;
using Xamarin.Forms;

namespace MicrosoftNews
{
    public partial class App : Application
    {
        private static IContainer _container;

        public App(IModule[] platformSpecificModules)
        {
            PrepareContainer(platformSpecificModules);

            InitializeComponent();
            var mainPage = new MainPage();
            mainPage.BindingContext = 
                new MainViewModel(mainPage.Navigation, _container.Resolve<IRepository<NewsModel>>(), _container.Resolve<IRepository<TimeStampModel>>());
            MainPage = new NavigationPage(mainPage);
        }

        private static void PrepareContainer(IModule[] platformSpecificModules)
        {
            var containerBuilder = new ContainerBuilder();
            RegisterPlatformSpecificModules(platformSpecificModules, containerBuilder);
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).SingleInstance();

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
