using System;
using MicrosoftNews.Constants;

namespace MicrosoftNews.Models
{
    public class TimeStampModel
    {
        public int Key { get; set; }

        public DateTime DateUpdated { get; set; }

        public TimeStampModel() { }
        public TimeStampModel(AppConstants.TimeStampKeys key, DateTime dateUpdated)
        {
            Key = (int)key;
            DateUpdated = dateUpdated;
        }
    }
}
