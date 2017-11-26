using MicrosoftNews.Constants;

namespace MicrosoftNews.Models
{
    public class TimeStamp
    {
        public int Key { get; set; }

        public string DateUpdated { get; set; }

        public TimeStamp() { }
        public TimeStamp(AppConstants.TimeStampKeys key, string dateUpdated)
        {
            Key = (int)key;
            DateUpdated = dateUpdated;
        }
    }
}
