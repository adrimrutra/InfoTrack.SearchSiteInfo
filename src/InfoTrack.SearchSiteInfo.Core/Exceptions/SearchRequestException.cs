namespace InfoTrack.SearchSiteInfo.Kernel.Exceptions;
public class SearchRequestException : Exception
{
  public SearchRequestException()
  {
  }

  public SearchRequestException(string message)
      : base(message)
  {
  }

  public SearchRequestException(string message, Exception inner)
  : base(message, inner)
  {
  }
}
