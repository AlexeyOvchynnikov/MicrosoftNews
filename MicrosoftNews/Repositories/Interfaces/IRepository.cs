using System.Collections.Generic;

namespace MicrosoftNews.Repositories.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        void CreateRepository();
        void DropRepository();
        List<T> GetAll();
        void Insert(T entity);
        void Insert(List<T> entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
