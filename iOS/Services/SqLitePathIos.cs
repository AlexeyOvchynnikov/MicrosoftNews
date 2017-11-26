using System;
using System.IO;
using MicrosoftNews.Interfaces;

namespace MicrosoftNews.iOS.Services
{
    public class SqLitePathIos : ISqLitePath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(documentsPath, sqliteFilename);
        }
    }
}
