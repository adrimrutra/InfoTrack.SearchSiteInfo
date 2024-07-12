using FluentValidation;
using InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace InfoTrack.SearchSiteInfo.UseCases;
public static class UseCasesServiceExtensions
{
  public static void AddUseCasesServices(this IServiceCollection services, ILogger logger)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    services.AddScoped<IValidator<CreateSearchRequestCommand>, CreateSearchRequestValidator>();
    

    logger.LogInformation("{Project} services registered", "UseCases");
  }
}
