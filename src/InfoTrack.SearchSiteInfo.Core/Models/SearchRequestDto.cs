using System.ComponentModel.DataAnnotations;
namespace InfoTrack.SearchSiteInfo.Core.Models;
public class SearchRequestDto
{
  public required string Keywords { get; set; }
  public required string Url { get; set; }
  public required string Engine { get; set; }
  public required int Count { get; set; }
}
