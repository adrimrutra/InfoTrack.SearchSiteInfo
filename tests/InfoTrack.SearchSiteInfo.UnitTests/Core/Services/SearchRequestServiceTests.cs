using InfoTrack.SearchSiteInfo.Core.Services;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using NSubstitute;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Xunit;
using Moq;

namespace InfoTrack.SearchSiteInfo.UnitTests.Core.Services;
public class SearchRequestServiceTests
{
  private readonly Mock<ISearchRequestHandler> _searchRequestHandler;
  private readonly Mock<ILogger<SearchRequestService>> _logger;

  public SearchRequestServiceTests()
  {
    _searchRequestHandler = new ();
    _logger = new ();
  }

  [Fact]
  public void GetSearchRequest_WhenSearchRequestIsValid_ShouldReturnSearchRequest()
  {
    // Arrange
    var searchRequest = new SearchRequestDto
    {      
      Keywords = "test",
      Engine = Engine.GOOGLE,
      Url = "test.com",
      Count = 100
    };

    var cancellationToken = new CancellationToken();
    var searchRequestHandler =  Substitute.For<ISearchRequestHandler>();

    _searchRequestHandler.Setup(x => x.HandleSearchRequestAsync(searchRequest, cancellationToken)).ReturnsAsync(10);

    var service = new SearchRequestService(_searchRequestHandler.Object, _logger.Object);

    // Act
    var result = service.CreateSearchRequestAsync(searchRequest, new CancellationToken())?.Result;

    // Assert
    result.Should().Be(10);

    var error = $"Successfully Create Search Request Async with request:{searchRequest}";

    _logger.Verify(x => x.Log(
          Microsoft.Extensions.Logging.LogLevel.Information,
          It.IsAny<EventId>(),
          It.IsAny<It.IsAnyType>(),
          It.IsAny<Exception>(),
          It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
          Times.Once);
  }
}
