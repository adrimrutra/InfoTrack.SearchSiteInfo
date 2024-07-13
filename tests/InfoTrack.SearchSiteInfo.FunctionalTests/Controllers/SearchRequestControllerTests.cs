using System.Net;
using System.Text;
using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Commons.Bases;
using InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
using Newtonsoft.Json;
using Xunit;

namespace InfoTrack.SearchSiteInfo.FunctionalTests.Controllers;
public class SearchRequestControllerTests : BaseControllerTests
{
  private readonly HttpClient _client;

  public SearchRequestControllerTests(CustomWebApplicationFactory<Program> factory)
   : base(factory)
  {
    _client = this.GetNewClient();
  }

  [Fact]
  public async Task CreateSearchRequestAsync_ReturnsOK()
  {
    // Arrange
    var request = new CreateSearchRequestCommand
    {
      Keywords = "land registry searches",
      Url = "www.infotrack.co.uk",
      Engine = Engine.GOOGLE,
    };

    var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

    // Act
    var response = await _client.PostAsync("/api/Search", stringContent);

    // Assert
    response.EnsureSuccessStatusCode();
    var responseString = await response.Content.ReadAsStringAsync();
    var result = JsonConvert.DeserializeObject<BaseResponse<SearchRequestViewModel>>(responseString);

    response.StatusCode.Should().Be(HttpStatusCode.OK);

    result.Should().NotBeNull();
    result?.Data.Should().NotBeNull();
    result?.Data?.Keywords.Should().Be(request.Keywords);
    result?.Data?.Url.Should().Be(request.Url);
    result?.Data?.Engine.Should().Be(request.Engine);
    result?.Data?.Rank.Should().BeGreaterThan(0);
  }

  [Fact]
  public async Task CreateSearchRequestAsync_ReturnsBadRequest()
  {
    // Arrange
    var request = new CreateSearchRequestCommand
    {
      Keywords = "",
      Url = "",
      Engine = "",
    };

    var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

    // Act
    var response = await _client.PostAsync("/api/Search", stringContent);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
  }
}
