using Xunit;

namespace InfoTrack.SearchSiteInfo.FunctionalTests.Controllers;
public class BaseControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
  private readonly CustomWebApplicationFactory<Program> _factory;

  public BaseControllerTests(CustomWebApplicationFactory<Program> factory)
  {
    _factory = factory;
  }

  public HttpClient GetNewClient()
  {
    var newClient = _factory.WithWebHostBuilder(builder =>
    {
      _factory.CustomConfigureServices(builder);
    }).CreateClient();

    return newClient;
  }

  public void Dispose()
  {
    _factory.Dispose();
  }
}
