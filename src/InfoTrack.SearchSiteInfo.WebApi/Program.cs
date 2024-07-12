using InfoTrack.SearchSiteInfo.Infrastructure.Data;
using InfoTrack.SearchSiteInfo.UseCases;
using InfoTrack.SearchSiteInfo.Infrastructure;
using Serilog;
using Serilog.Extensions.Logging;
using InfoTrack.SearchSiteInfo.WebApi.Infrastructure;
using InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
using FluentValidation;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

logger.Information("Starting web host");

// AddAsync services to the container.
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                      policy.WithOrigins("http://localhost:4200")
                      .AllowAnyMethod()
                      .AllowAnyHeader();
                    });
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddUseCasesServices(microsoftLogger);

builder.Services.AddInfrastructureServices(builder.Configuration, microsoftLogger);

builder.Services.AddHttpClient();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

SeedDatabase(app);

app.Run();


static void SeedDatabase(WebApplication app)
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}
