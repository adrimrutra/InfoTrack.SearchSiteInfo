namespace InfoTrack.SearchSiteInfo.Core.Models;
public class SearchRequestViewModel
{
  public int Id { get; set; }
  public required string Keywords { get; set; }

  public required string Url { get; set; }

  public required string Engine { get; set; }

  public required int Rank { get; set; }

  public required string CreatedDate { get; set; }
}
