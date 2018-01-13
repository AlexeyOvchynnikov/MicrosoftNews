using System.Collections.ObjectModel;
using System.Linq;
using MicrosoftNews.Constants;
using MicrosoftNews.Models;
using MicrosoftNews.ViewModels.Core;
using MicrosoftNews.Views;
using Xamarin.Forms;
using MicrosoftNews.Services;
using MicrosoftNews.Repositories.Interfaces;

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
        private readonly IRepository<NewsModel> _newsRepository;
        private readonly IRepository<TimeStampModel> _timeStampRepository;

        public ObservableCollection<NewsModel> NewsItems { get => _newsItems; set => SetProperty(ref _newsItems, value); }
        public string ChannelTitle { get => _channelTitle; set => SetProperty(ref _channelTitle, value); }
        public string ChannelDescription { get => _channelDescription; set => SetProperty(ref _channelDescription, value); }

        public MainViewModel(INavigation navigationService, IRepository<NewsModel> newsRepository, IRepository<TimeStampModel> timeStampRepository) : base(navigationService)
        {
            _newsRepository = newsRepository;
            _timeStampRepository = timeStampRepository;
            Load();
        }

        private void Load()
        {

            _feedParser = new FeedParser(_feedUrl);
            FeedModel _feed = _feedParser.GetFeed();
            ChannelTitle = _feed.Title;
            ChannelDescription = _feed.Description;

            var localUpdateTime = _timeStampRepository.GetAll()
                                     .FirstOrDefault(t => t.Key.Equals((int)AppConstants.TimeStampKeys.News))?.DateUpdated;
            var serverUpdateTime = _feed.LastUpdatedDate;

            if (serverUpdateTime == localUpdateTime)
            {
                var localNews = _newsRepository.GetAll();
                NewsItems = new ObservableCollection<NewsModel>(localNews);
                return;
            }


            var newsList = _feedParser.GetNews();
            _newsRepository.DropRepository();
            _newsRepository.CreateRepository();
            _newsRepository.Insert(newsList);
            NewsItems = new ObservableCollection<NewsModel>(newsList);

            _timeStampRepository.Insert(new TimeStampModel(AppConstants.TimeStampKeys.News, serverUpdateTime));
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
