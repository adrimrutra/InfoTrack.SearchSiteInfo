using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Models;
using Microsoft.Extensions.Logging;

namespace InfoTrack.SearchSiteInfo.Core.Services;
public class SearchRequestService(ISearchRequestHandler _searchRequestHandler, ILogger<SearchRequestService> _logger) 
  : ISearchRequestService
{

  public async Task<int> CreateSearchRequestAsync(SearchRequestDto request, CancellationToken cancellationToken)
  {
    var result = await _searchRequestHandler.HandleSearchRequestAsync(request, cancellationToken);   
     _logger.LogInformation("Successfully Create Search Request Async with request:{request}", request);

    return result;
  }
}
