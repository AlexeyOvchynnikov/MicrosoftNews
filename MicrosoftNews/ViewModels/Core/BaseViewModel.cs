using Xamarin.Forms;

namespace MicrosoftNews.ViewModels.Core
{
    class BaseViewModel : ObservableObject
    {
        protected INavigation NavigationService { get; set; }
        protected BaseViewModel(INavigation navigationService)
        {
            NavigationService = navigationService;
            Initialize();
        }
        protected virtual void Initialize() { }
    }
}
