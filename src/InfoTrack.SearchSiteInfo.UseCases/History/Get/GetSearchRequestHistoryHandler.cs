using AutoMapper;
using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Commons.Bases;
using MediatR;

namespace InfoTrack.SearchSiteInfo.UseCases.History.Get;
public class GetSearchRequestHistoryHandler(IUnitOfWork _unitOfWork, IMapper _mapper) :
  IRequestHandler<GetSearchRequestHistoryQuery, BaseResponse<IEnumerable<SearchRequestViewModel>>>
{
  public async Task<BaseResponse<IEnumerable<SearchRequestViewModel>>> Handle(
    GetSearchRequestHistoryQuery request,
    CancellationToken cancellationToken)


  {
    var response = new BaseResponse<IEnumerable<SearchRequestViewModel>>();

    var repository = _unitOfWork.GetRepository<SearchRequest>();
    var searchRequests = await repository.GetAllAsync(cancellationToken);

    response.Data = _mapper.Map<IEnumerable<SearchRequestViewModel>>(searchRequests);
    response.Succcess = true;
    response.Message = "Search Request History Query succeed!";

    return response;
  }
}
