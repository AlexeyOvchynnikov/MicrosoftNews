﻿using System;

namespace MicrosoftNews.Models
{
    public class NewsModel
    {
        private DateTime _publishedDate;

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedDate { get => _publishedDate; set => _publishedDate = value; }

        public string PublishedDateString => _publishedDate.ToString("dd MMMM yyyy");
    }
}
