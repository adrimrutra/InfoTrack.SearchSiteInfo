using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Commons.Bases;
using MediatR;

namespace InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
public class CreateSearchRequestCommand : IRequest<BaseResponse<SearchRequestViewModel>>
{
  public required string Keywords { get; set; }
  public required string Url { get; set; }
  public required string Engine { get; set; }
}

