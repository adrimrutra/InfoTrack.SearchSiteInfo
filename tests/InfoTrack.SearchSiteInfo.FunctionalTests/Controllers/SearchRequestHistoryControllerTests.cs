using System.Net;
using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Commons.Bases;
using Newtonsoft.Json;
using Xunit;


namespace InfoTrack.SearchSiteInfo.FunctionalTests.Controllers;
public class SearchRequestHistoryControllerTests : BaseControllerTests
{
  private readonly HttpClient _client;
  public SearchRequestHistoryControllerTests(CustomWebApplicationFactory<Program> factory)
    : base(factory)
  {
    _client = this.GetNewClient();
  }

  [Fact]
  public async Task GetAllAsync_ReturnsAllSearchRequests()
  {
    // Arrange, Act
    var response = await _client.GetAsync("/api/History/GetAll");

    // Assert
    response.EnsureSuccessStatusCode();

    var stringResponse = await response.Content.ReadAsStringAsync();
    var result = JsonConvert.DeserializeObject<BaseResponse<IEnumerable<SearchRequestViewModel>>>(stringResponse);

    response.StatusCode.Should().Be(HttpStatusCode.OK);

    result.Should().NotBeNull();
    result?.Data.Should().NotBeNullOrEmpty();
    result?.Data?.Count().Should().Be(10);
  }
}
