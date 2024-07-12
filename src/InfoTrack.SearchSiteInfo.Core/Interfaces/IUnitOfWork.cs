namespace InfoTrack.SearchSiteInfo.Core.Interfaces;
public interface IUnitOfWork : IDisposable
{
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

  IRepository<T> GetRepository<T>() where T : class;
}
