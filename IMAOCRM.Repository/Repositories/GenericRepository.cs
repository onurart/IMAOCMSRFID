using IMAOCMS.Core.Repositories;
using IMAOCMS.Core.Utilities.Abstract;
using IMAOCMS.Core.Utilities.Concrete;
using IMAOCMS.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMAOCRM.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<IDataResult<T>> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return new SuccessDataResult<T>(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<IDataResult<T>> GetByIdAsync(int id)
        {
            return new SuccessDataResult<T>(await _dbSet.FindAsync(id));
        }

        public async Task<IDataResult<List<T>>> GetListAsync()
        {
            return new SuccessDataResult<List<T>>(await _dbSet.ToListAsync());
        }

        public async Task<IDataResult<List<T>>> GetListTestAsync()
        {
            return new SuccessDataResult<List<T>>(await _dbSet.ToListAsync());
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<Result> RemoveSoft(T entity)
        {
            if (entity.GetType().GetProperty("IsDelete") != null)
            {
                T _entity = entity;
                _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);
                this.Update(_entity);
            }
            return new SuccessResult(ResultMessages.Deleted);
        }

        public IDataResult<T> Update(T entity)
        {
            _dbSet.Update(entity);
            return new SuccessDataResult<T>(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
