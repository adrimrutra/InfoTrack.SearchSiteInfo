using InfoTrack.SearchSiteInfo.Core.Constants;
namespace InfoTrack.SearchSiteInfo.Core.Interfaces;
public interface ISearchEngineService
{
  string CreateUrl(string engine, string keywords, int count);
  string GetSplitter(string engine);
  void AddDefaultRequestHeaders(HttpClient httpClient, string engine);
}
