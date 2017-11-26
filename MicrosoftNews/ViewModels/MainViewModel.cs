using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MicrosoftNews.Constants;
using MicrosoftNews.Models;
using MicrosoftNews.ViewModels.Core;
using MicrosoftNews.Views;
using Suyati.FeedAggreagator;
using Xamarin.Forms;
using System.Xml;

namespace MicrosoftNews.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private readonly string _feedUrl = "https://news.microsoft.com/feed/";

        private RSSFeed _rssFeed;
        private ObservableCollection<News> _newsItems;
        private News _selectedNews;
        private string _newsTitle;

        public RSSFeed RssFeed
        {
            get => _rssFeed;
            set => SetProperty(ref _rssFeed, value);
        }

        public ObservableCollection<News> NewsItems
        {
            get => _newsItems;
            set => SetProperty(ref _newsItems, value);
        }

        public string NewsTitle
        {
            get => _newsTitle;
            set => SetProperty(ref _newsTitle, value);
        }

        public MainViewModel(INavigation navigationService) : base(navigationService)
        {
        }

        protected override void Initialize()
        {
            bool isValidFeed = SyndicationFeed.IsValidFeed(_feedUrl);
            if (!isValidFeed)
            {
                return;
            }

            var syndicationFeed = SyndicationFeed.Load(_feedUrl);
            _rssFeed = (RSSFeed)syndicationFeed.Feed;
            NewsTitle = _rssFeed.Title;
            var feed = syndicationFeed.Feed;

            var localUpdateTime = App.TimeStampRepository.GetAll().FirstOrDefault(t => t.Key.Equals((int)AppConstants.TimeStampKeys.News))?.DateUpdated;
            var serverUpdateTime = feed.LastUpdatedDate.ToString();
            var newsItems = new List<News>();
            if (serverUpdateTime == localUpdateTime)
            {
                var localNews = App.NewsRepository.GetAll();
                foreach (var item in localNews)
                {
                    newsItems.Add(item);
                }

                NewsItems = new ObservableCollection<News>(newsItems);
                return;
            }
            App.NewsRepository.DropRepository();
            App.NewsRepository.CreateRepository();

            foreach (var item in feed.Items)
            {
                newsItems.Add(new News
                {
                    Title = item.Title,
                    Description = item.Description,
                    PublishedDate = (DateTime)item.PublishedDate,
                    Url = item.Url
                });
            }
            App.NewsRepository.Insert(newsItems);

            NewsItems = new ObservableCollection<News>(newsItems);
            App.TimeStampRepository.Insert(new TimeStamp(AppConstants.TimeStampKeys.News, serverUpdateTime));

        }

        public News SelectedNews
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
