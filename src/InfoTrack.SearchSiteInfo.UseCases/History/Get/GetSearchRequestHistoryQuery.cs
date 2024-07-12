using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Commons.Bases;
using MediatR;

namespace InfoTrack.SearchSiteInfo.UseCases.History.Get;

public class GetSearchRequestHistoryQuery : IRequest<BaseResponse<IEnumerable<SearchRequestViewModel>>>
{
}
