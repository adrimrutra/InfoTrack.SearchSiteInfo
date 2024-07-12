using InfoTrack.SearchSiteInfo.Core.Extensions;
using InfoTrack.SearchSiteInfo.Core.Interfaces;

namespace InfoTrack.SearchSiteInfo.Core.Services;
public class SearchResponseParserService : ISearchResponseParserService
{
  public int ParseSearchResponse(string response, string splitter, string url)
  {
    string[] subContents = response.Split(splitter);

    foreach (var (content, index) in subContents.SelectWithIndex())
    {
      if (content.Contains(url))
      {
        return index;
      }
    }
    return 0;
  }
}
