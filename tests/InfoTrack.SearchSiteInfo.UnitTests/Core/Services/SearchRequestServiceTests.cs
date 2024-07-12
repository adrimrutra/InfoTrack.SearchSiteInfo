using InfoTrack.SearchSiteInfo.Core.Services;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using NSubstitute;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using FluentAssertions;

namespace InfoTrack.SearchSiteInfo.UnitTests.Core.Services;
public class SearchRequestServiceTests
{
  [Test]
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

    searchRequestHandler.HandleSearchRequestAsync(searchRequest, cancellationToken).Returns(10);

    var logger =  Substitute.For<ILogger<SearchRequestService>>();

    var service = new SearchRequestService(searchRequestHandler, logger);

    // Act
    var result = service.CreateSearchRequestAsync(searchRequest, new CancellationToken())?.Result;

    // Assert
    result.Should().Be(10);

  //  logger.Received().LogInformation("Successfully Create Search Request Async with request:{request}", searchRequest);

    //logger.Received().Log(
    //       LogLevel.Error,
    //       Arg.Any<EventId>(),
    //       Arg.Is<object>(o => o.ToString() == logError),
    //       null,
    //       Arg.Any<Func<object, Exception?, string>>());
  }
}
