using IMAOCMS.Core.Utilities.Abstract;
using IMAOCMS.Core.Utilities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMAOCMS.Core.Services
{
    public interface IServices<T> where T : class
    {
        Task<IDataResult<T>> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IDataResult<List<T>>> GetListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<IDataResult<T>> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        Task<IDataResult<T>> UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task<Result> RemoveSoftAsync(T entity);
        Task<IDataResult<List<T>>> GetListTestAsync();
    }
}
