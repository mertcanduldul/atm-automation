using System.Linq.Expressions;
using Automation.Core.Repository;
using Automation.Core.Service;
using Automation.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Automation.Service.Service;

public class Service<T> : IService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<T> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAll().ToListAsync();

    public IQueryable<T> Where(Expression<Func<T, bool>> expression) => _repository.Where(expression);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => await _repository.AnyAsync(expression);

    public async Task<T> AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        return entities;
    }

    public async Task UpdateAsync(T entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        _repository.Remove(entity);
        await _unitOfWork.CommitAsync();
    }

    public Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        _repository.RemoveRange(entities);
        return _unitOfWork.CommitAsync();
    }
}