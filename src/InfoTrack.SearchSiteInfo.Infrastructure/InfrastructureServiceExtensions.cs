using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Core.Heandlers;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Services;
using InfoTrack.SearchSiteInfo.Infrastructure.Data;
using InfoTrack.SearchSiteInfo.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfoTrack.SearchSiteInfo.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("InfoTrackSearchSiteInfoDb");
    services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

    services.AddTransient<IRepository<SearchRequest>, Repository<SearchRequest>>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<ISearchRequestService, SearchRequestService>();
    services.AddScoped<ISearchRequestHandler, SearchRequestHandler>();
    services.AddScoped<ISearchResponseParserService, SearchResponseParserService>();
    services.AddScoped<ISearchEngineService, SearchEngineService>();

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
