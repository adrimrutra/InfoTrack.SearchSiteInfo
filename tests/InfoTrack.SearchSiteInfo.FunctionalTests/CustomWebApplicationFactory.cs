using InfoTrack.SearchSiteInfo.Infrastructure.Data;
using InfoTrack.SearchSiteInfo.KernelSetupTests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfoTrack.SearchSiteInfo.FunctionalTests;
public class CustomWebApplicationFactory<TStartup> :  WebApplicationFactory<TStartup> where TStartup : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      var descriptor = services.SingleOrDefault(
          d => d.ServiceType ==
              typeof(DbContextOptions<AppDbContext>));

      if (descriptor != null)
      {
        services.Remove(descriptor);
      }

      services.AddDbContext<AppDbContext>(options =>
      {
        options.UseInMemoryDatabase("InfoTrackSearchSiteInfoTestDb");
      });

      var serviceProvider = services.BuildServiceProvider();

      using (var scope = serviceProvider.CreateScope())
      {
        var scopedServices = scope.ServiceProvider;

        var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

        var storeDbContext = scopedServices.GetRequiredService<AppDbContext>();
        storeDbContext.Database.EnsureCreated();

        try
        {
          DatabaseSetup.SeedData(storeDbContext);
        }
        catch (Exception ex)
        {
          logger.LogError(ex, $"An error occurred seeding the Store database with test messages. Error: {ex.Message}");
        }
      }
    });
  }

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);   
  }

  public void CustomConfigureServices(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      var serviceProvider = services.BuildServiceProvider();

      using (var scope = serviceProvider.CreateScope())
      {
        var scopedServices = scope.ServiceProvider;

        var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

        var storeDbContext = scopedServices.GetRequiredService<AppDbContext>();

        try
        {
          DatabaseSetup.SeedData(storeDbContext);
        }
        catch (Exception ex)
        {
          logger.LogError(ex, $"An error occurred seeding the Store database with test messages. Error: {ex.Message}");
        }
      }
    });
  }
}
