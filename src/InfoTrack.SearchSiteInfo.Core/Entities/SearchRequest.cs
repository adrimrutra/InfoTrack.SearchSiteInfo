﻿namespace InfoTrack.SearchSiteInfo.Core.Entities;
public class SearchRequest : EntityBase
{
  public required string Keywords { get; set; }

  public required string Url { get; set; }

  public required string Engine { get; set; }

  public required int Rank { get; set; }

  public DateTimeOffset CreatedDate { get; set; }
}
