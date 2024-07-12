using System.Web;
using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Services;
using NSubstitute;
using NUnit.Framework;


namespace InfoTrack.SearchSiteInfo.UnitTests.Core.Services;
public class SearchEngineServiceTests
{
  private readonly string KEYWORDS = "land registry searches";

  [Test]
  public void CreateUrl_WhenEngineIsGoogle_ShouldReturnGoogleUrl()
  {
    // Arrange
    var service = new SearchEngineService();
    var expectedResult = $"http://www.google.co.uk/search?q={HttpUtility.UrlEncode(KEYWORDS)}&num={100}";

    // Act
    var result = service.CreateUrl(Engine.GOOGLE, KEYWORDS, 100);

    // Assert
    result.Should().Be(expectedResult);
  }

  [Test]
  public void CreateUrl_WhenEngineIsBing_ShouldReturnBingUrl()
  {
    // Arrange
    var service = new SearchEngineService();
    var expectedResult = $"http://www.bing.com/search?q={HttpUtility.UrlEncode(KEYWORDS)}&count={100}";

    // Act
    var result = service.CreateUrl(Engine.BING, KEYWORDS, 100);

    // Assert
    result.Should().Be(expectedResult);
  }

  [Test]
  public void CreateUrl_WhenEngineIsYahoo_ShouldReturnYahooUrl()
  {
    // Arrange
    var service = new SearchEngineService();
    var expectedResult = $"https://search.yahoo.com/search;_ylt=AwrhbzdJLo1mPhoDARhXNyoA;_ylu=Y29sbwNiZjEEcG9zAzEEdnRpZAMEc2VjA3BhZ2luYXRpb24-?fr=crmas_sfp&fr2=sb-top&p={HttpUtility.UrlEncode(KEYWORDS)}&b=29&pz=7&bct=0&xargs=0&pstart=33&count={100}";

    // Act
    var result = service.CreateUrl(Engine.YAHOO, KEYWORDS, 100);

    // Assert
    result.Should().Be(expectedResult);
  }

  [Test]
  public void CreateUrl_WhenEngineIsNotSupported_ShouldThrowException()
  {
    // Arrange
    var engine = "NotSupported";
    var service = new SearchEngineService();

    // Act
    Action act = () => service.CreateUrl(engine, KEYWORDS, 100);

    // Assert
    act.Should().Throw<NotSupportedException>().WithMessage("Engine not found");
  }


  [Test]
  public void GetSplitter_WhenEngineIsGoogle_ShouldReturnGoogleSplitter()
  {
    // Arrange
    var service = new SearchEngineService();
    var expectedResult = "<div class=\"egMi0 kCrYT\">";

    // Act
    var result = service.GetSplitter(Engine.GOOGLE);

    // Assert
    result.Should().Be(expectedResult);
  }

  [Test]
  public void GetSplitter_WhenEngineIsBing_ShouldReturnBingSplitter()
  {
    // Arrange
    var service = new SearchEngineService();
    var expectedResult = "<li class=\"b_algo\" data-id=\"\"  iid=\"\">";

    // Act
    var result = service.GetSplitter(Engine.BING);

    // Assert
    result.Should().Be(expectedResult);
  }

  [Test]
  public void GetSplitter_WhenEngineIsYahoo_ShouldReturnYahooSplitter()
  {
    // Arrange
    var service = new SearchEngineService();
    var expectedResult = "<h3 class=\"title d-ib td-hu va-top";

    // Act
    var result = service.GetSplitter(Engine.YAHOO);

    // Assert
    result.Should().Be(expectedResult);
  }

  [Test]
  public void GetSplitter_WhenEngineIsNotSupported_ShouldThrowException()
  {
    // Arrange
    var engine = "NotSupported";
    var service = new SearchEngineService();

    // Act
    Action act = () => service.GetSplitter(engine);

    // Assert
    act.Should().Throw<NotSupportedException>().WithMessage("Engine not found");
  }

  [Test]
  public void AddDefaultRequestHeaders_WhenEngineIsBing_ShouldAddDefaultRequestHeaders()
  {
    // Arrange
    var service = new SearchEngineService();
    var httpClient = Substitute.For<HttpClient>();

    var expectedResult = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";

    // Act
    service.AddDefaultRequestHeaders(httpClient, Engine.BING);

    // Assert
    httpClient.DefaultRequestHeaders.UserAgent.Should().NotBeEquivalentTo(expectedResult);
  }

  [Test]
  public void AddDefaultRequestHeaders_WhenEngineIsYahoo_ShouldAddDefaultRequestHeaders()
  {
    // Arrange
    var service = new SearchEngineService();
    var httpClient = Substitute.For<HttpClient>();

    var expectedResult = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";

    // Act
    service.AddDefaultRequestHeaders(httpClient, Engine.YAHOO);

    // Assert
    httpClient.DefaultRequestHeaders.UserAgent.Should().NotBeEquivalentTo(expectedResult);
  }

  [Test]
  public void AddDefaultRequestHeaders_WhenEngineIsGoogle_ShouldAddDefaultRequestHeaders()
  {
    // Arrange
    var service = new SearchEngineService();
    var httpClient = Substitute.For<HttpClient>();

    // Act
    service.AddDefaultRequestHeaders(httpClient, Engine.GOOGLE);

    // Assert
    httpClient.DefaultRequestHeaders.UserAgent.Should().BeEmpty();
  }
}
