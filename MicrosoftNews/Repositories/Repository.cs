using System.Collections.Generic;
using System.Linq;
using MicrosoftNews.Interfaces;
using MicrosoftNews.Repositories.Interfaces;
using SQLite;

namespace MicrosoftNews.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private SQLiteConnection _database;

        public Repository(ISqLitePath sqLitePath)
        {
            string databasePath = sqLitePath.GetDatabasePath(Constants.AppConstants.UserDbFileName);
            _database = new SQLiteConnection(databasePath);
            CreateRepository();
        }

        public void CreateRepository()
        {
            _database.CreateTable<T>();
        }

        public void DropRepository()
        {

            _database.DropTable<T>();
        }

        public void Delete(T entity)
        {
            _database.Delete(entity);
        }

        public List<T> GetAll()
        {
            var newsList = _database.Table<T>().ToList();
            return newsList;
        }

        public void Insert(T entity)
        {
            _database.Insert(entity);
        }

        public void Update(T entity)
        {
            _database.Update(entity);
        }

        public void Insert(List<T> entities)
        {
            foreach (var entity in entities)
            {
                _database.Insert(entity);
            }
        }
    }
}
