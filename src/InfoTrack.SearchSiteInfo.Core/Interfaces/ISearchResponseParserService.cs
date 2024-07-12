namespace InfoTrack.SearchSiteInfo.Core.Interfaces;
public interface ISearchResponseParserService
{
  int ParseSearchResponse(string content, string splitter, string url);
}
