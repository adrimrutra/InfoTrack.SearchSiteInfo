using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.SearchSiteInfo.WebApi.Infrastructure;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
  {
    _logger.LogError(exception, "Exception has occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
          Title = "Bad Request",
          Status = StatusCodes.Status500InternalServerError,
          Instance = "API",
          Detail = $"API Error {exception.Message}" ,
          Type = "Server Error",
        };
    var problemJson = JsonSerializer.Serialize(problemDetails);
    httpContext.Response.ContentType = "application/problem+json";
    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    await httpContext.Response.WriteAsync(problemJson, cancellationToken);

    return true;
  }
}

