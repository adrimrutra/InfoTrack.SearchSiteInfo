using System.Web;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Interfaces;

namespace InfoTrack.SearchSiteInfo.Core.Services;
public class SearchEngineService : ISearchEngineService
{
  public string CreateUrl(string engine, string keywords, int count)
  {
    return engine switch
    {
      Engine.GOOGLE => $"http://www.google.co.uk/search?q={HttpUtility.UrlEncode(keywords)}&num={count}",
      Engine.BING => $"http://www.bing.com/search?q={HttpUtility.UrlEncode(keywords)}&count={count}",
      Engine.YAHOO => $"https://search.yahoo.com/search;_ylt=AwrhbzdJLo1mPhoDARhXNyoA;_ylu=Y29sbwNiZjEEcG9zAzEEdnRpZAMEc2VjA3BhZ2luYXRpb24-?fr=crmas_sfp&fr2=sb-top&p={HttpUtility.UrlEncode(keywords)}&b=29&pz=7&bct=0&xargs=0&pstart=33&count={count}",
      _ => throw new NotSupportedException("Engine not found")  
    };
  }

  public string GetSplitter(string engine)
  {
    return engine switch
    {
      Engine.GOOGLE => Splitter.GOOGLE,
      Engine.BING => Splitter.BING,
      Engine.YAHOO => Splitter.YAHOO,
      _ => throw new NotSupportedException("Engine not found")
    };
  }

  public void AddDefaultRequestHeaders(HttpClient httpClient, string engine)
  {
    if (Engine.GOOGLE != engine) {
      httpClient.DefaultRequestHeaders.Add(
        "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36"
        );
    }     
  }
}
