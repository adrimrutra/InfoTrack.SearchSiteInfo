using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Services;
using NUnit.Framework;

namespace InfoTrack.SearchSiteInfo.UnitTests.Core.Services;
public class SearchResponseParserServiceTests
{
  [Test]
  public void ParseSearchResponse_WhenResponseContainsUrl_ShouldReturnIndex()
  {
    // Arrange
    var response = "test1,test2,test3,test4,test5";
    var splitter = ",";
    var url = "test3";
    var service = new SearchResponseParserService();

    // Act
    var result = service.ParseSearchResponse(response, splitter, url);

    // Assert
    result.Should().Be(2);
  }

  [Test]
  public void ParseSearchResponse_WhenResponseNotContainsUrl_ShouldReturnZero()
  {
    // Arrange
    var response = "test1 test2 test3 test4 test5";
    var splitter = " ";
    var url = "test6";
    var service = new SearchResponseParserService();

    // Act
    var result = service.ParseSearchResponse(response, splitter, url);

    // Assert
    result.Should().Be(0);
  }
}
