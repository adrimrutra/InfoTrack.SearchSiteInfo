namespace InfoTrack.SearchSiteInfo.Core.Extensions;
public static class EnumerableExtensions
{
  public static IEnumerable<(T, int)> SelectWithIndex<T>(this IEnumerable<T> rows)
  {
    return rows.Select((value, index) => (value, index));
  }
}
