using System.Collections.ObjectModel;
using System.Linq;
using MicrosoftNews.Constants;
using MicrosoftNews.Models;
using MicrosoftNews.ViewModels.Core;
using MicrosoftNews.Views;
using Xamarin.Forms;
using MicrosoftNews.Services;

namespace MicrosoftNews.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private readonly string _feedUrl = "https://news.microsoft.com/feed/";
        private FeedParser _feedParser;
        private ObservableCollection<NewsModel> _newsItems;
        private NewsModel _selectedNews;
        private string _channelTitle;
        private string _channelDescription;

        public ObservableCollection<NewsModel> NewsItems { get => _newsItems; set => SetProperty(ref _newsItems, value); }
        public string ChannelTitle { get => _channelTitle; set => SetProperty(ref _channelTitle, value); }
        public string ChannelDescription { get => _channelDescription; set => SetProperty(ref _channelDescription, value); }
       
        public MainViewModel(INavigation navigationService) : base(navigationService)
        {
        }

        protected override void Initialize()
        {
            _feedParser = new FeedParser(_feedUrl);
            FeedModel _feed = _feedParser.GetFeed();

            ChannelTitle = _feed.Title;
            ChannelDescription = _feed.Description;

            var localUpdateTime = App.TimeStampRepository.GetAll()
                                     .FirstOrDefault(t => t.Key.Equals((int)AppConstants.TimeStampKeys.News))?.DateUpdated;
            var serverUpdateTime = _feed.LastUpdatedDate;

            if (serverUpdateTime == localUpdateTime)
            {
                var localNews = App.NewsRepository.GetAll();
                NewsItems = new ObservableCollection<NewsModel>(localNews);
                return;
            }


            var newsList = _feedParser.GetNews();
            App.NewsRepository.DropRepository();
            App.NewsRepository.CreateRepository();
            App.NewsRepository.Insert(newsList);
            NewsItems = new ObservableCollection<NewsModel>(newsList);

            App.TimeStampRepository.Insert(new TimeStampModel(AppConstants.TimeStampKeys.News, serverUpdateTime));
        }

        public NewsModel SelectedNews
        {
            set
            {
                SetProperty(ref _selectedNews, value);
                if (value == null) return;
                if (NavigationService.NavigationStack.All(x => x.GetType() != typeof(NewsDetailsPage)))
                {
                    NavigationService.PushAsync(new NewsDetailsPage
                    {
                        BindingContext = new NewsDetailsViewModel(NavigationService, _selectedNews)
                    });
                    SelectedNews = null;
                }
            }
        }
    }
}
