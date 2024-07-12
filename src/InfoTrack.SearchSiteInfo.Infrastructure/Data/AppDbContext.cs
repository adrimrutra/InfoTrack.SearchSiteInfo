using InfoTrack.SearchSiteInfo.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.SearchSiteInfo.Infrastructure.Data;
public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
  {
  }

  public DbSet<SearchRequest> SearchRequests => Set<SearchRequest>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
}
