using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T: class, IEntityBase, new()
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);

        public Task<T> GetByIdAsync(int Id);

        public Task<T> UpdateAsync(int id, T entity);

        public Task DeleteAsync(int id);

        public Task AddAsync(T entity);
    }
}
