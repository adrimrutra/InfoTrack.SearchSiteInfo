using InfoTrack.SearchSiteInfo.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.SearchSiteInfo.Infrastructure.Data.Repositories;
public sealed class Repository<T> : IRepository<T> where T : class
{
  private readonly AppDbContext _context;
  private readonly DbSet<T> _dbSet;

  public Repository(AppDbContext context)
  {
    _context = context;
    _dbSet = _context.Set<T>();
  }
  public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
  {
    await _dbSet.AddAsync(entity, cancellationToken);
  }

  public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    return await _dbSet.ToListAsync(cancellationToken);
  }
}
