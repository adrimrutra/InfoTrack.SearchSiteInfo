
using System.Globalization;
using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Infrastructure.Data;
using InfoTrack.SearchSiteInfo.Infrastructure.Data.Repositories;
using InfoTrack.SearchSiteInfo.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace InfoTrack.SearchSiteInfo.UnitTests.Infrastructure.Data.Repositories;
public class RepositoryTests : IDisposable
{
  private readonly AppDbContext _context;
  private readonly DbContextOptions<AppDbContext> _options;
  private readonly UnitOfWork _unitOfWork;

  private readonly List<SearchRequest> searchRequests;
  private readonly SearchRequest searchRequest1 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 2, CreatedDate = DateTimeOffset.Parse("01/01/2023 12:00", CultureInfo.CurrentCulture) };
  private readonly SearchRequest searchRequest2 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.BING, Keywords = "land registry searches", Rank = 10, CreatedDate = DateTimeOffset.Parse("02/02/2023 12:00", CultureInfo.CurrentCulture) };
  private readonly SearchRequest searchRequest3 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.YAHOO, Keywords = "land registry searches", Rank = 18, CreatedDate = DateTimeOffset.Parse("03/03/2023 12:00", CultureInfo.CurrentCulture) };

  public RepositoryTests()
  {
    _options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "InfoTrackSearchSiteInfoTestDb").Options;
    _context = new AppDbContext(_options);
    _unitOfWork = new UnitOfWork(_context);
    searchRequests = new List<SearchRequest>() { searchRequest1, searchRequest2, searchRequest3 };
  }

  [Test]
  public async Task AddAsync_WhenEntityIsValid_ShouldAddEntityToDbSet()
  {
    // Arrange
    var searchRequest = new SearchRequest()
    {
      Url = "www.infotrack.co.uk",
      Engine = Engine.GOOGLE,
      Keywords = "land registry searches",
      Rank = 45,
      CreatedDate = DateTimeOffset.Parse("04/04/2023 12:00", CultureInfo.CurrentCulture)
    };

    var cancellationToken = new CancellationToken();
    var repository = _unitOfWork.GetRepository<SearchRequest>();

    // Act
    await repository.AddAsync(searchRequest, cancellationToken);
    var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

    // Assert
    result.Should().Be(1);
  }

  [Test]
  public async Task GetAllAsync_WhenDbSetIsNotEmpty_ShouldReturnAllEntities()
  {
    // Arrange
    SetupDataBase();
    var cancellationToken = new CancellationToken();
    var repository = _unitOfWork.GetRepository<SearchRequest>();

    // Act
    var result = await repository.GetAllAsync(cancellationToken);

    // Assert
    result.Should().HaveCount(3);

    foreach (var (value, index) in result.SelectWithIndex())
    {
      value.Should().BeEquivalentTo(searchRequests[index]);
    }
  }

  [Test]
  public async Task GetAllAsync_WhenDbSetIsEmpty_ShouldReturnEmptyCollection()
  {
    // Arrange
    SetupDataBase();
    var cancellationToken = new CancellationToken();
    var repository = _unitOfWork.GetRepository<SearchRequest>();

    // Act
    var result = await repository.GetAllAsync(cancellationToken);

    // Assert
    result.Should().BeEmpty();
  }

  private void SetupDataBase()
  {
    if (!_context.SearchRequests.Any())
    {
      _context.SearchRequests.AddRange(searchRequest1, searchRequest2, searchRequest3);
      _context.SaveChanges();
    }
    else
    {
      _context.SearchRequests.RemoveRange(_context.SearchRequests);
      _context.SaveChanges();
    }
  }

  public void Dispose()
  {
    _context.Database.EnsureDeleted();
    _context.Dispose();
    GC.SuppressFinalize(this);
  }
}
