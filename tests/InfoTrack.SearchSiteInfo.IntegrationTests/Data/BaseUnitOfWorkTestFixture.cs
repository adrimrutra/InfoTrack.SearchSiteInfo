using InfoTrack.SearchSiteInfo.Infrastructure.Data;
using InfoTrack.SearchSiteInfo.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.SearchSiteInfo.IntegrationTests.Data;

public abstract class BaseUnitOfWorkTestFixture : IDisposable
{
  protected readonly UnitOfWork _unitOfWork;

  protected BaseUnitOfWorkTestFixture()
  {
    var options = CreateNewContextOptions();
    _unitOfWork = new UnitOfWork(new AppDbContext(options));
  }

  protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
  {
    var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();

    var builder = new DbContextOptionsBuilder<AppDbContext>();
    builder.UseInMemoryDatabase("InfoTrackSearchSiteInfoTestDb")
           .UseInternalServiceProvider(serviceProvider);

    return builder.Options;
  }

  protected UnitOfWork GetUnitOfWork()
  {
    return _unitOfWork;
  }

  public void Dispose()
  {
    _unitOfWork.Dispose();
  } 
}
