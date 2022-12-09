using System.Linq.Expressions;

namespace WebApi.Data.ChequeDirect.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<T> GetById(string id);
        Task<T> GetById(int[] id);
        Task<T> GetById(string[] id);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}