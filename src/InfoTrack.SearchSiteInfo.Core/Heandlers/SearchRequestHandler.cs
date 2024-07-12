using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.Kernel.Exceptions;
using Microsoft.Extensions.Logging;

namespace InfoTrack.SearchSiteInfo.Core.Heandlers;
public class SearchRequestHandler(
  HttpClient _httpClient,
  ISearchEngineService _searchEngineHelper,
  ISearchResponseParserService _searchResponseParserService,
  ILogger<SearchRequestHandler> _logger) : ISearchRequestHandler
{
  public async Task<int> HandleSearchRequestAsync(SearchRequestDto request, CancellationToken cancellationToken)
  {
    try
    {
      var url = _searchEngineHelper.CreateUrl(request.Engine, request.Keywords, request.Count);
      _searchEngineHelper.AddDefaultRequestHeaders(_httpClient, request.Engine);

      var response = await _httpClient.GetStringAsync(url, cancellationToken);
      var rank = _searchResponseParserService.ParseSearchResponse(response, _searchEngineHelper.GetSplitter(request.Engine), request.Url);

      _logger.LogInformation("Successfully Handle Search Request Async with request: {request}", request);
      return rank;
    }
    catch (Exception exception)
    {
      throw new SearchRequestException("Something went wrong while Handle Search Request Async", exception);
    }

  }
}
