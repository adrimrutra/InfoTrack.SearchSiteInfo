using InfoTrack.SearchSiteInfo.Core.Interfaces;

namespace InfoTrack.SearchSiteInfo.Infrastructure.Data.Repositories;
public class UnitOfWork : IUnitOfWork
{
  private readonly AppDbContext _context;
  public UnitOfWork(AppDbContext context)
  {
    _context = context;
  }

  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return await _context.SaveChangesAsync(cancellationToken);
  }

  public IRepository<T> GetRepository<T>() where T : class
  {
    return new Repository<T>(_context);
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}
