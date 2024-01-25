using System.Linq.Expressions;

namespace OrderAPI.Application.Common.Interfaces;

public interface IGenericRepository<T> where T : class
{
    T? GetById(int Id);
    Task<T?> GetByIdAsync(int Id);
    IQueryable<T> GetAll();
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    Task<IEnumerable<T>> GetPaginatedListAsync(int page, int size);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    void SaveChanges();
    void Rollback();
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync();
}