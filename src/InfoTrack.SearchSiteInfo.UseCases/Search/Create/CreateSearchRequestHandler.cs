using AutoMapper;
using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Commons.Bases;
using MediatR;

namespace InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
public class CreateSearchRequestHandler(CreateSearchRequestValidator _searchRequestValidator, ISearchRequestService _searchService, IUnitOfWork _unitOfWork, IMapper _mapper)
  : IRequestHandler<CreateSearchRequestCommand, BaseResponse<SearchRequestViewModel>>
{
  async Task<BaseResponse<SearchRequestViewModel>> IRequestHandler<CreateSearchRequestCommand, BaseResponse<SearchRequestViewModel>>
    .Handle(CreateSearchRequestCommand request, CancellationToken cancellationToken)
  {
    var validationResult = await _searchRequestValidator.ValidateAsync(request, cancellationToken);

    var response = new BaseResponse<SearchRequestViewModel>();
    if (!validationResult.IsValid)
    {
      response.Succcess = false;
      response.Message = "Create Search Request Command failed!";
      response.Errors = validationResult.Errors.Select(x => new BaseError { ErrorMessage = x.ErrorMessage, PropertyMessage = x.PropertyName });
      return response;
    }

    var searchRequestDto = _mapper.Map<SearchRequestDto>(request);
    var result = await _searchService.CreateSearchRequestAsync(searchRequestDto, cancellationToken);

    var searchRequest = _mapper.Map<SearchRequest>(searchRequestDto);
    searchRequest.Rank = result;

    var repository = _unitOfWork.GetRepository<SearchRequest>();

    await repository.AddAsync(searchRequest, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    response.Data = _mapper.Map<SearchRequestViewModel>(searchRequest);
    response.Succcess = true;
    response.Message = "Create Search Request Command succeed!";

    return response;
  }
}
