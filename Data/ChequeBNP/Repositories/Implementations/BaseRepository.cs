
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Data.ChequeBNP.Repositories.Interfaces;

namespace WebApi.Data.ChequeBNP.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ChequeBNP _dbcontext;

        public BaseRepository(ChequeBNP dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public virtual async Task Add(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
        }

        public virtual async Task AddRange(IEnumerable<T> entities)
        {
            await _dbcontext.Set<T>().AddRangeAsync(entities);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbcontext.Set<T>().Where(expression);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            var item = await _dbcontext.Set<T>().FindAsync(id);
            if (item is null)
            {
                throw new ArgumentNullException($"Get by id is null {item}");
            }
            return item;
        }

        public virtual async Task<T> GetById(string id)
        {
            var item = await _dbcontext.Set<T>().FindAsync(id);
            if (item is null)
            {
                throw new ArgumentNullException($"Get by id is null {item}");
            }
            return item;
        }

        public virtual async Task<T> GetById(int[] id)
        {
            var item = await _dbcontext.Set<T>().FindAsync(String.Join(",", id));
            if (item is null)
            {
                throw new ArgumentNullException($"Get by id is null {item}");
            }
            return item;
        }

        public virtual async Task<T> GetById(string[] id)
        {
            // throw new NotImplementedException();
            var item = await _dbcontext.Set<T>().FindAsync(String.Join(",", id));
            if (item is null)
            {
                throw new ArgumentNullException($"Get by id is null {item}");
            }
            return item;
            
        }

        public virtual void Remove(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _dbcontext.Set<T>().RemoveRange(entities);
        }
    }
}