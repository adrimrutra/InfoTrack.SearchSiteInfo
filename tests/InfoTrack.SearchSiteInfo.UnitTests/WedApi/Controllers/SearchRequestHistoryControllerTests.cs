﻿using System.Globalization;
using AutoMapper;
using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Core.Interfaces;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Commons.Bases;
using InfoTrack.SearchSiteInfo.UseCases.History.Get;
using InfoTrack.SearchSiteInfo.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
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

    var searchRequestViewModel = new List<SearchRequestViewModel>()
    {
      new SearchRequestViewModel { Id = 1, Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 2, CreatedDate = "01/01/2023 12:00"},
      new SearchRequestViewModel { Id = 2, Url = "www.infotrack.co.uk", Engine = Engine.BING, Keywords = "land registry searches", Rank = 10, CreatedDate = "02/02/2023 12:00" },
      new SearchRequestViewModel { Id = 3, Url = "www.infotrack.co.uk", Engine = Engine.YAHOO, Keywords = "land registry searches", Rank = 18, CreatedDate = "03/03/2023 12:00"}
    };

    var expectedResult = new BaseResponse<IEnumerable<SearchRequestViewModel>>()
    {
      Succcess = true,
      Message = "Create Search Request Command succeed!",      
    };
    expectedResult.Data = searchRequestViewModel;

     var cancellationToken = new CancellationToken();

    _mockMapper.Setup(m => m.Map<IEnumerable<SearchRequestViewModel>>(It.IsAny<IEnumerable<SearchRequest>>())).Returns(searchRequestViewModel);

    _mockRepository.Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(searchRequests);
    _mockUnitOfWork.Setup(m => m.GetRepository<SearchRequest>()).Returns(_mockRepository.Object);
    _mockMediator.Setup(m => m.Send<BaseResponse<IEnumerable<SearchRequestViewModel>>>(
        It.IsAny<GetSearchRequestHistoryQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult).Verifiable();

    var controller = new SearchRequestHistoryController(_mockMediator.Object);
    // Act
    var result = await controller.GetAllAsync(cancellationToken);

    // Assert
    result.Should().NotBeNull();
    result.Should().BeOfType<OkObjectResult>();

    var okObjectResult = result as OkObjectResult;
    okObjectResult?.StatusCode.Should().Be(200);
    okObjectResult?.Value.Should().BeEquivalentTo(expectedResult);
  }

  [Fact]
  public async void GetAllAsync_WhenCalled_ReturnsBadRequestResult()
  {
    // Arrange
    var expectedResult = new BaseResponse<IEnumerable<SearchRequestViewModel>>()
    {
      Succcess = false,
      Message = "Create Search Request Command failed!",
    };

    var cancellationToken = new CancellationToken();

    _mockMediator.Setup(m => m.Send<BaseResponse<IEnumerable<SearchRequestViewModel>>>(
             It.IsAny<GetSearchRequestHistoryQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult).Verifiable();

    var controller = new SearchRequestHistoryController(_mockMediator.Object);
    // Act
    var result = await controller.GetAllAsync(cancellationToken);

    // Assert
    result.Should().NotBeNull();
    result.Should().BeOfType<BadRequestObjectResult>();

    var badRequestObjectResult = result as BadRequestObjectResult;
    badRequestObjectResult?.StatusCode.Should().Be(400);
    badRequestObjectResult?.Value.Should().BeEquivalentTo(expectedResult);
  }
}
