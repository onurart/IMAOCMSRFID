using IMAOCMS.Core.Repositories;
using IMAOCMS.Core.Services;
using IMAOCMS.Core.UnitOfWorks;
using IMAOCMS.Core.Utilities.Abstract;
using IMAOCMS.Core.Utilities.Concrete;
using IMAOCMS.Repository.Constants;
using IMAOCMS.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IMAOCRM.Service.Service
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public Services(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Add(T entity)
        {
            _repository.Add(entity);
            _unitOfWork.Commit();
        }

        public async Task<IDataResult<T>> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return new SuccessDataResult<T>(entity, ResultMessages.Added);
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return _repository.AnyAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<IDataResult<T>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result is null)
            {
                throw new NotFoundException($"{typeof(T).Name}({id}) Not Found");
            }
            return new SuccessDataResult<T>(result.Data, ResultMessages.GetId);
        }

        public async Task<IDataResult<List<T>>> GetListAsync()
        {
            var result = await _repository.GetListAsync();
            return result;
        }

        public async Task<IDataResult<List<T>>> GetListTestAsync()
        {
            var result = await _repository.GetListTestAsync();
            return result;
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Result> RemoveSoftAsync(T entity)
        {
            await _repository.RemoveSoft(entity);
            await _unitOfWork.CommitAsync();
            return new SuccessResult(ResultMessages.Deleted);
        }

        public async Task<IDataResult<T>> UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return new SuccessDataResult<T>(entity, ResultMessages.Updated);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _repository.Where(predicate);
        }
    }
}
