using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Commons.Bases;
using InfoTrack.SearchSiteInfo.UseCases.History.Get;
using InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
using InfoTrack.SearchSiteInfo.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using NSubstitute.Extensions;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace InfoTrack.SearchSiteInfo.UnitTests.WedApi.Controllers;
public class SearchRequestHistoryControllerTests
{
  private readonly Mock<IMapper> _mockMapper = new();
  private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();
  private readonly Mock<IRepository<SearchRequest>> _mockRepository = new();
  private readonly Mock<IMediator> _mockMediator = new();

  [Fact]
  public async void GetAllAsync_WhenCalled_ReturnsOkResult()
  {
    // Arrange
    var searchRequests = new List<SearchRequest>
    {
      new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 2, CreatedDate = DateTimeOffset.Parse("01/01/2023 12:00", CultureInfo.CurrentCulture) },
      new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.BING, Keywords = "land registry searches", Rank = 10, CreatedDate = DateTimeOffset.Parse("02/02/2023 12:00", CultureInfo.CurrentCulture) },
      new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.YAHOO, Keywords = "land registry searches", Rank = 18, CreatedDate = DateTimeOffset.Parse("03/03/2023 12:00", CultureInfo.CurrentCulture) }
    };


   
    var searchRequestViewModel = new List<SearchRequestViewModel>
    {
      new SearchRequestViewModel { Id = 1, Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 2, CreatedDate = "01/01/2023 12:00"},
      new SearchRequestViewModel { Id = 2, Url = "www.infotrack.co.uk", Engine = Engine.BING, Keywords = "land registry searches", Rank = 10, CreatedDate = "02/02/2023 12:00" },
      new SearchRequestViewModel { Id = 3, Url = "www.infotrack.co.uk", Engine = Engine.YAHOO, Keywords = "land registry searches", Rank = 18, CreatedDate = "03/03/2023 12:00"}
    };

    var expectedResult = new BaseResponse<SearchRequestViewModel>()
    {
      Data = null,
      Succcess = true,
      Message = "Create Search Request Command succeed!"
    };

    var cancellationToken = new CancellationToken();

    _mockMapper.Setup(m => m.Map<IEnumerable<SearchRequestViewModel>>(It.IsAny<IEnumerable<SearchRequest>>())).Returns(searchRequestViewModel);

    var _mockMediator = Substitute.For<IMediator>();
    _mockMediator.Send(new GetSearchRequestHistoryQuery(), cancellationToken).Returns(expectedResult);
   // _mockMediator.Send(new GetSearchRequestHistoryQuery(), cancellationToken).ReturnsForAll(expectedResult);

    


    _mockRepository.Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>()))
      .ReturnsAsync(searchRequests);
    _mockUnitOfWork.Setup(m => m.GetRepository<SearchRequest>())
      .Returns(_mockRepository.Object);

    //  object value = _mockMediator.Setup(m => m.Send(new GetSearchRequestHistoryQuery(), cancellationToken)).ReturnsAsync(expectedResult);
    var _sut = new SearchRequestHistoryController(_mockMediator);


  //  _mockMediator.Setup(i => i.Send(cancellationToken)).ReturnsAsync(expectedResult);
   // _mockMediator.Setup(i => i.Send(It.IsAny<GetSearchRequestHistoryQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

    // Act
    var result = await _sut.GetAllAsync(cancellationToken);

    // Assert
    Assert.NotNull(result);
  }
}
