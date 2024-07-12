using InfoTrack.SearchSiteInfo.Core.Models;

namespace InfoTrack.SearchSiteInfo.Core.Interfaces;
public interface ISearchRequestHandler
{
  Task<int> HandleSearchRequestAsync(SearchRequestDto request, CancellationToken cancellationToken);
}
