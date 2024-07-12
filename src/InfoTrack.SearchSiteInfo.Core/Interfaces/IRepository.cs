namespace InfoTrack.SearchSiteInfo.Core.Interfaces;
public interface IRepository<TEntity> where TEntity : class
{
  Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
  Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}
