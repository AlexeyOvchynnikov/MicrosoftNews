using MicrosoftNews.Models;
using MicrosoftNews.ViewModels.Core;
using Xamarin.Forms;

namespace MicrosoftNews.ViewModels
{
    class NewsDetailsViewModel : BaseViewModel
    {
        private string _title;
        private string _publishedDateString;
        private string _description;
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string PublishedDateString { get => _publishedDateString; set => SetProperty(ref _publishedDateString, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public NewsDetailsViewModel(INavigation navigationService, NewsModel newsModel) : base(navigationService)
        {
            Title = newsModel.Title;
            PublishedDateString = newsModel.PublishedDateString;
            Description = newsModel.Description;
        }
    }
}
