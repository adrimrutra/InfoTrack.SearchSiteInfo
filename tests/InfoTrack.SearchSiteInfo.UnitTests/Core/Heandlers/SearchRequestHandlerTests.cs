using System.Net;
using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Heandlers;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.Kernel.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InfoTrack.SearchSiteInfo.UnitTests.Core.Heandlers;
public class SearchRequestHandlerTests
{ 
  private readonly MockHttpMessageHandler _mockHttpMessageHandler = new MockHttpMessageHandler();
  private readonly HttpClient _httpClient;
  private readonly Mock<ISearchEngineService> _searchEngineHelper;
  private readonly Mock<ISearchResponseParserService> _searchResponseParserService;
  private readonly Mock<ILogger<SearchRequestHandler>> _logger;
  private readonly SearchRequestHandler _searchRequestHandler;

  public SearchRequestHandlerTests()
  {
    _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
    _searchEngineHelper = new Mock<ISearchEngineService>();
    _searchResponseParserService = new Mock<ISearchResponseParserService>();
    _logger = new Mock<ILogger<SearchRequestHandler>>();
    _searchRequestHandler = new SearchRequestHandler(_httpClient, _searchEngineHelper.Object, _searchResponseParserService.Object, _logger.Object);
  }

  [Fact]
  public async Task HandleSearchRequestAsync_WhenCalled_ShouldReturnRank()
  {
    // Arrange
    var request = new SearchRequestDto
    {
      Keywords = "land registry searches",
      Url = "www.infotrack.co.uk",
      Engine = Engine.GOOGLE,
      Count = 100
    };

    var response = "<html><body><a href=\"www.infotrack.co.uk\"></a></body></html>";
    var rank = 1;

    _searchEngineHelper.Setup(x => x.CreateUrl(request.Engine, request.Keywords, request.Count))
      .Returns("https://www.google.com/search?q=test&num=100");
    _searchEngineHelper.Setup(x => x.GetSplitter(request.Engine))
      .Returns("href=\"");

    var responseContent = new StringContent(response);
    _mockHttpMessageHandler.ReturnsResponse(responseContent, "text/plain", HttpStatusCode.OK);

    _searchResponseParserService.Setup(x => x.ParseSearchResponse(response, "href=\"", request.Url)).Returns(rank);

    // Act
    var result = await _searchRequestHandler.HandleSearchRequestAsync(request, CancellationToken.None);

    // Assert
    result.Should().Be(rank);

    _logger.Verify(x => x.Log(
          Microsoft.Extensions.Logging.LogLevel.Information,
          It.IsAny<EventId>(),
          It.IsAny<It.IsAnyType>(),
          It.IsAny<Exception>(),
          It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
          Times.Once);
  }

  [Fact]
  public async Task HandleSearchRequestAsync_WhenExceptionThrown_ShouldThrowSearchRequestException()
  {
    // Arrange
    var request = new SearchRequestDto
    {
      Keywords = "land registry searches",
      Url = "www.infotrack.co.uk",
      Engine = Engine.GOOGLE,
      Count = 100
    };

    var response = "";
    var rank = -1;

    _searchEngineHelper.Setup(x => x.CreateUrl(request.Engine, request.Keywords, request.Count))
      .Returns("https://www.google.com/search?q=test&num=100");
    _searchEngineHelper.Setup(x => x.GetSplitter(request.Engine))
      .Returns("href=\"");

    var responseContent = new StringContent(response);
    _mockHttpMessageHandler.ReturnsResponse(responseContent, "text/plain", HttpStatusCode.BadRequest);

    _searchResponseParserService.Setup(x => x.ParseSearchResponse(response, "href=\"", request.Url)).Returns(rank);

    // Act
    Func<Task> act = async () => await _searchRequestHandler.HandleSearchRequestAsync(request, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<SearchRequestException>().WithMessage("Something went wrong while Handle Search Request Async");
  }
}


