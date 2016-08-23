using ClientsAPI.Domain.Models;
using System.Collections.Generic;

namespace ClientsAPI.Data.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        void Insert(T entity);
        void Update(T entity);
        IEnumerable<T> GetAll();
        T FindById(string Id);
    }
}
