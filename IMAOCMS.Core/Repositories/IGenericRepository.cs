using IMAOCMS.Core.Utilities.Abstract;
using IMAOCMS.Core.Utilities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMAOCMS.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IDataResult<T>> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task<IDataResult<List<T>>> GetListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<IDataResult<T>> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        IDataResult<T> Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<Result> RemoveSoft(T entity);
        Task<IDataResult<List<T>>> GetListTestAsync();
    }
}
