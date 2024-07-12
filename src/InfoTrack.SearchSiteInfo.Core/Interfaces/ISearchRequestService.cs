using InfoTrack.SearchSiteInfo.Core.Models;

namespace InfoTrack.SearchSiteInfo.Core.Interfaces;
public interface ISearchRequestService
{
  Task<int> CreateSearchRequestAsync(SearchRequestDto request, CancellationToken cancellationToken);
}
