using System;
using System.Collections.Generic;
using System.Xml.Linq;
using MicrosoftNews.Models;

namespace MicrosoftNews.Services
{
    public class FeedParser
    {
        XDocument _feedDoc;
        XElement _channel;

        public FeedParser(string feedUrl)
        {

            _feedDoc = XDocument.Load(feedUrl);
            _channel = _feedDoc.Root.Element("channel");
        }

        public FeedModel GetFeed()
        {
            return new FeedModel
            {
                Title = _channel.Element("title").Value,
                Description = _channel.Element("description").Value,
                LastUpdatedDate = DateTime.Parse(_channel.Element("lastBuildDate").Value)
            };
        }

        public List<NewsModel> GetNews()
        {
            var newsList = new List<NewsModel>();

            foreach (var element in _channel.Elements("item"))
            {
                newsList.Add(new NewsModel
                {
                    Title = element.Element("title").Value,
                    Description = element.Element("description").Value,
                    PublishedDate = DateTime.Parse(element.Element("pubDate").Value)
                });
            }
            return newsList;
        }
    }
}
