using System.Globalization;
using AutoMapper;
using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.History.Get;
using Moq;
using Xunit;

namespace InfoTrack.SearchSiteInfo.UnitTests.UseCases.History.Get;
public class GetSearchRequestHistoryHandlerTests
{
  private readonly GetSearchRequestHistoryHandler _sut;
  private readonly Mock<IMapper> _mockMapper = new();
  private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();
  private readonly Mock<IRepository<SearchRequest>> _mockRepository = new();

  public GetSearchRequestHistoryHandlerTests()
  {
    _sut = new GetSearchRequestHistoryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
  }

  [Fact]
  public async Task Handle_WhenCalled_ReturnSearchRequestHistory()
  {
    // Arrange
    var searchRequests = new List<SearchRequest>
    {
      new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 2, CreatedDate = DateTimeOffset.Parse("01/01/2023 12:00", CultureInfo.CurrentCulture) },
      new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.BING, Keywords = "land registry searches", Rank = 10, CreatedDate = DateTimeOffset.Parse("02/02/2023 12:00", CultureInfo.CurrentCulture) },
      new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.YAHOO, Keywords = "land registry searches", Rank = 18, CreatedDate = DateTimeOffset.Parse("03/03/2023 12:00", CultureInfo.CurrentCulture) }
    };

    var expectedResult = new List<SearchRequestViewModel>
    {
      new SearchRequestViewModel { Id = 1, Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 2, CreatedDate = "01/01/2023 12:00"},
      new SearchRequestViewModel { Id = 2, Url = "www.infotrack.co.uk", Engine = Engine.BING, Keywords = "land registry searches", Rank = 10, CreatedDate = "02/02/2023 12:00" },
      new SearchRequestViewModel { Id = 3, Url = "www.infotrack.co.uk", Engine = Engine.YAHOO, Keywords = "land registry searches", Rank = 18, CreatedDate = "03/03/2023 12:00"}
    };


    _mockMapper.Setup(m => m.Map<IEnumerable<SearchRequestViewModel>>(It.IsAny<IEnumerable<SearchRequest>>())).Returns(expectedResult);
    _mockRepository.Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>()))
      .ReturnsAsync(searchRequests);
    _mockUnitOfWork.Setup(m => m.GetRepository<SearchRequest>())
      .Returns(_mockRepository.Object);

    var query = new GetSearchRequestHistoryQuery();

    // Act
    var result = await _sut.Handle(query, CancellationToken.None);

    // Assert
    result.Should().NotBeNull();
    result.Succcess.Should().BeTrue();
    result.Message.Should().Be("Search Request History Query succeed!");
    result.Data.Should().NotBeNull();
    result.Data.Should().HaveCount(3);
    result.Data.Should().BeEquivalentTo(expectedResult);
  }

  [Fact]
  public async Task Handle_WhenCalled_ReturnEmptySearchRequestHistory()
  {
    // Arrange
    var searchRequests = new List<SearchRequest>();

    _mockMapper.Setup(m => m.Map<IEnumerable<SearchRequestViewModel>>(It.IsAny<IEnumerable<SearchRequest>>()))
      .Returns(new List<SearchRequestViewModel>());
    _mockRepository.Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>()))
      .ReturnsAsync(searchRequests);
    _mockUnitOfWork.Setup(m => m.GetRepository<SearchRequest>())
      .Returns(_mockRepository.Object);

    var query = new GetSearchRequestHistoryQuery();

    // Act
    var result = await _sut.Handle(query, CancellationToken.None);

    // Assert

    result.Should().NotBeNull();
    result.Succcess.Should().BeTrue();
    result.Data.Should().NotBeNull();
    result.Data.Should().BeEmpty();
  }
}
