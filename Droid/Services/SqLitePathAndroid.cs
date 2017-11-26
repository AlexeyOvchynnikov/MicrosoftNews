using System.IO;
using MicrosoftNews.Interfaces;

namespace MicrosoftNews.Droid.Services
{
    public class SqLitePathAndroid : ISqLitePath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(documentsPath, sqliteFilename);
        }
    }
}
