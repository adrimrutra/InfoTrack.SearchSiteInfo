using AutoMapper;
using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Commons.Bases;
using InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
using MediatR;
using Moq;
using Xunit;

namespace InfoTrack.SearchSiteInfo.UnitTests.UseCases.Search.Create;
public class CreateSearchRequestHandlerTests
{
  private readonly CreateSearchRequestHandler _sut;
  private readonly CreateSearchRequestValidator _createSearchRequestValidator = new CreateSearchRequestValidator();
  private readonly Mock<ISearchRequestService> _searchService = new();
  private readonly Mock<IUnitOfWork> _unitOfWork = new();
  private readonly Mock<IMapper> _mapper = new();
  private readonly Mock<IRepository<SearchRequest>> _repository = new();

  public CreateSearchRequestHandlerTests()
  {
    _sut = new CreateSearchRequestHandler(_createSearchRequestValidator, _searchService.Object, _unitOfWork.Object, _mapper.Object);
  }

  [Fact]
  public async void Handle_WhenCalled_ReturnBaseResponse_SearchResponseViewModel()
  {
    // Arrange
    var request = new CreateSearchRequestCommand
    {
      Keywords = "land registry searches",
      Url = "www.infotrack.co.uk",
      Engine = Engine.GOOGLE
    };

    var searchRequestDto = new SearchRequestDto
    {
      Keywords = "land registry searches",
      Engine = Engine.GOOGLE,
      Url = "www.infotrack.co.uk",
      Count = 100
    };

    CancellationToken cancellationToken = new();

    _mapper.Setup(x => x.Map<SearchRequestDto>(request)).Returns(searchRequestDto);
    _searchService.Setup(x => x.CreateSearchRequestAsync(searchRequestDto, CancellationToken.None)).ReturnsAsync(1);

    var searchRequest = new SearchRequest
    {
      Keywords = searchRequestDto.Keywords,
      Engine = searchRequestDto.Engine,
      Url = searchRequestDto.Url,
      Rank = 1,
      CreatedDate = DateTimeOffset.Now
    };

    _mapper.Setup(x => x.Map<SearchRequest>(searchRequestDto)).Returns(searchRequest);

    _unitOfWork.Setup(x => x.GetRepository<SearchRequest>()).Returns(_repository.Object);
    _repository.Setup(x => x.AddAsync(searchRequest, cancellationToken)).Returns(Task.CompletedTask);
    _unitOfWork.Setup(x => x.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

    var searchRequestViewModel = new SearchRequestViewModel
    {
      Id = 1,
      Keywords = searchRequest.Keywords,
      Engine = searchRequest.Engine.ToString(),
      Url = searchRequest.Url,
      Rank = searchRequest.Rank,
      CreatedDate = searchRequest.CreatedDate.ToString()
    };

    _mapper.Setup(x => x.Map<SearchRequestViewModel>(searchRequest)).Returns(searchRequestViewModel);

    var response = new BaseResponse<SearchRequestViewModel>()
    {
      Data = searchRequestViewModel,
      Succcess = true,
      Message = "Create Search Request Command succeed!"
    };

    // Act
    var result = await (_sut as IRequestHandler<CreateSearchRequestCommand, BaseResponse<SearchRequestViewModel>>).Handle(request, cancellationToken);

    // Assert
    result.Should().NotBeNull();
    result.Should().BeEquivalentTo(response);
    result.Should().BeEquivalentTo(response, options => options.Excluding(x => x.Data));
    result.Should().BeEquivalentTo(response, options => options.Excluding(x => x.Succcess));
    result.Should().BeEquivalentTo(response, options => options.Excluding(x => x.Message));
  }

  [Fact]
  public async void Handle_Returns_Error_When_Validation_Fails()
  {
    // Arrange
    var request = new CreateSearchRequestCommand
    {
      Keywords = "",
      Url = "",
      Engine = ""
    };

    CancellationToken cancellationToken = new();
    List<BaseError> errors = new List<BaseError>() {
        new BaseError { ErrorMessage = "'Keywords' must not be empty.", PropertyMessage = "Keywords" },
        new BaseError { ErrorMessage = "'Url' must not be empty.", PropertyMessage = "Url" },
        new BaseError { ErrorMessage = "Incorrect Url", PropertyMessage = "Url" },
        new BaseError { ErrorMessage = "'Engine' must not be empty.", PropertyMessage = "Engine" },
        new BaseError { ErrorMessage = "Incorrect Engine", PropertyMessage = "Engine" }
    };

    var expectedResult = new BaseResponse<SearchRequestViewModel>()
    {
      Succcess = false,
      Message = "Create Search Request Command failed!",
      Errors = errors.Select(x => new BaseError { ErrorMessage = x.ErrorMessage, PropertyMessage = x.PropertyMessage })
    };

    // Act
    var result = await (_sut as IRequestHandler<CreateSearchRequestCommand, BaseResponse<SearchRequestViewModel>>).Handle(request, cancellationToken);

    // Assert
    result.Should().NotBeNull();
    result.Should().BeEquivalentTo(expectedResult);
    result.Should().BeEquivalentTo(expectedResult, options => options.Excluding(x => x.Succcess));
    result.Should().BeEquivalentTo(expectedResult, options => options.Excluding(x => x.Message));
    result.Should().BeEquivalentTo(expectedResult, options => options.Excluding(x => x.Errors));
  }
}
