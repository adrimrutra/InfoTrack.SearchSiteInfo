using System.Web;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Heandlers;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using Xunit;

namespace InfoTrack.SearchSiteInfo.UnitTests.Core.Heandlers;
public class SearchRequestHandlerTests
{
  private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler = new(MockBehavior.Strict);
  private readonly HttpClient _httpClient;

  public SearchRequestHandlerTests()
  {
    _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
  }

  [Fact]
  public async void HandleSearchRequestAsync()
  {
    // Arrange
    var response = "test1,test2,test3,test4,test5";
    var splitter = ",";

    var searchRequest = new SearchRequestDto
    {
      Keywords = "land registry searches",
      Engine = Engine.GOOGLE,
      Url = "www.infotrack.co.uk",
      Count = 100
    };

    var url = $"http://www.google.co.uk/search?q={HttpUtility.UrlEncode(searchRequest.Keywords)}&num={searchRequest.Count}";


    var cancellationToken = new CancellationToken();

    var _searchEngineHelper = Substitute.For<ISearchEngineService>();
    var _searchResponseParserService = Substitute.For<ISearchResponseParserService>();
    var _logger = Substitute.For<ILogger<SearchRequestHandler>>();

    var _searchRequestHandler = new SearchRequestHandler(_httpClient, _searchEngineHelper, _searchResponseParserService, _logger);


    _searchEngineHelper.CreateUrl(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()).Returns(url);
    _searchEngineHelper.AddDefaultRequestHeaders(_httpClient, It.IsAny<string>());
    _searchEngineHelper.GetSplitter(It.IsAny<string>()).Returns(splitter);
    _searchResponseParserService.ParseSearchResponse(response, splitter, url).Returns(2);

    // _httpClient.GetStringAsync(url, cancellationToken).Returns(response);


    // Act
    var result = await _searchRequestHandler.HandleSearchRequestAsync(searchRequest, cancellationToken);

    // Assert
    Assert.Equal(2, result);
  }

  //[Fact]
  //public void Test2()
  //{
  //  // Arrange
  //  var response = "test1 test2 test3 test4 test5";
  //  var splitter = " ";
  //  var url = "test6";
  //  var service = new SearchResponseParserService();

  //  // Act
  //  var result = service.ParseSearchResponse(response, splitter, url);

  //  // Assert
  //  Assert.Equal(0, result);
  //} 
}

//public class SearchRequestHandler(
//  HttpClient _httpClient,
//  ISearchEngineService _searchEngineHelper,
//  ISearchResponseParserService _searchResponseParserService,
//  ILogger<SearchRequestHandler> _logger) : ISearchRequestHandler
//{
//  public async Task<int> HandleSearchRequestAsync(SearchRequestDto request, CancellationToken cancellationToken)
//  {
//    try
//    {
//      string url = _searchEngineHelper.CreateUrl(request.Engine, request.Keywords, request.Count);
//      _searchEngineHelper.AddDefaultRequestHeaders(_httpClient, request.Engine);

//      var response = await _httpClient.GetStringAsync(url, cancellationToken);
//      var rank = _searchResponseParserService.ParseSearchResponse(response, _searchEngineHelper.GetSplitter(request.Engine), request.Url);

//      _logger.LogInformation("Successfully Handle Search Request Async with request: {request}", request);
//      return rank;
//    }
//    catch (Exception exception)
//    {
//      throw new SearchRequestException("Something went wrong while Handle Search Request Async", exception);
//    }

//  }
//}
