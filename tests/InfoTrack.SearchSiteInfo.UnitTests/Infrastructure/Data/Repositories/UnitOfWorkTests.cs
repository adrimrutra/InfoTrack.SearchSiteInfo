
using System.Globalization;
using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Infrastructure.Data;
using InfoTrack.SearchSiteInfo.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace InfoTrack.SearchSiteInfo.UnitTests.Infrastructure.Data.Repositories;
public class UnitOfWorkTests : IDisposable
{
  private readonly DbContextOptions<AppDbContext> _options;
  private readonly AppDbContext _context;
  private readonly SearchRequest searchRequest = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 2, CreatedDate = DateTimeOffset.Parse("01/01/2023 12:00", CultureInfo.CurrentCulture) };

  public UnitOfWorkTests()
  {
    _options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName: "InfoTrackSearchSiteInfoTestDb")
      .Options;

    _context = new AppDbContext(_options);
  }

  [Test]
  public void GetRepository_WhenCalled_ShouldReturnRepository()
  {
    // Arrange
    var unitOfWork = new UnitOfWork(_context);

    // Act
    var repository = unitOfWork.GetRepository<SearchRequest>();

    // Assert
    repository.Should().NotBeNull();
    repository.Should().BeOfType<Repository<SearchRequest>>();
  }

  [Test]
  public async Task SaveChangesAsync_WhenCalled_ShouldSaveChanges()
  {
    // Arrange
    var unitOfWork = new UnitOfWork(_context);
    var repository = unitOfWork.GetRepository<SearchRequest>();
    var cancellationToken = new CancellationToken();
    await repository.AddAsync(searchRequest, cancellationToken);

    // Act
    var result = await unitOfWork.SaveChangesAsync(cancellationToken);

    // Assert
    result.Should().Be(1);
  }
  public void Dispose()
  {
    _context.Database.EnsureDeleted();
    _context.Dispose();
    GC.SuppressFinalize(this);
  }
}
