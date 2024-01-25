using System.Linq.Expressions;
using OrderAPI.Application.Common.Interfaces;

namespace OrderAPI.Infrastructure.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly OrderDbContext _dbContext;
    private readonly DbSet<T> _entitiySet;

    public GenericRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
        _entitiySet = _dbContext.Set<T>();
    }

    public void Add(T entity)
    {
        _dbContext.Add(entity);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(entity, cancellationToken);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _dbContext.AddRange(entities);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddRangeAsync(entities, cancellationToken);
    }

    public IQueryable<T> GetAll()
    {
        return _entitiySet;
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
    {
        return _entitiySet.Where(expression);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _entitiySet.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _entitiySet.Where(expression).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetPaginatedListAsync(int page, int size)
    {
        return await _dbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
    }

    public void Remove(T entity)
    {
        _dbContext.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbContext.RemoveRange(entities);
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        _dbContext.UpdateRange(entities);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Rollback()
    {
        _dbContext.Dispose();
    }

    public async Task RollbackAsync()
    {
        await _dbContext.DisposeAsync();
    }

    public T? GetById(int Id)
    {
        return _entitiySet.Find(Id);
    }

    public async Task<T?> GetByIdAsync(int Id)
    {
        return await _entitiySet.FindAsync(Id);
    }
}
