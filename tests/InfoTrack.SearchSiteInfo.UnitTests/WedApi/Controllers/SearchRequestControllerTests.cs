using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Commons.Bases;
using InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
using InfoTrack.SearchSiteInfo.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace InfoTrack.SearchSiteInfo.UnitTests.WedApi.Controllers;
public class SearchRequestControllerTests
{
  [Test]
  public void CreateSearchRequestAsync_WhenCalled_ReturnsOkResult()
  {
    // Arrange
    var mediator = new Mock<IMediator>();
    var controller = new SearchRequestController(mediator.Object);
    var request = new CreateSearchRequestCommand()
    {
      Keywords = "land registry searches",
      Url = "www.infotrack.co.uk",
      Engine = "GOOGLE"
    };
    var cancellationToken = new CancellationToken();

    var expectedResult = new BaseResponse<SearchRequestViewModel>()
    {
      Data = new SearchRequestViewModel
      {
        Id = 1,
        Keywords = "land registry searches",
        Url = "www.infotrack.co.uk",
        Engine = "GOOGLE",
        Rank = 1,
        CreatedDate = DateTime.Now.ToString()
      },
      Succcess = true,
      Message = "Create Search Request Command succeed!",
    };

    mediator.Setup(m => m.Send(request, cancellationToken)).ReturnsAsync(expectedResult);

    // Act
    var actionResult =  controller.CreateSearchRequestAsync(request, cancellationToken)?.Result;

    // Assert
    actionResult.Should().NotBeNull();
    actionResult.Should().BeOfType<OkObjectResult>();

    var okObjectResult = actionResult as OkObjectResult;
    okObjectResult?.StatusCode.Should().Be(200);
    okObjectResult?.Value.Should().BeEquivalentTo(expectedResult);
  }

  [Test]
  public void CreateSearchRequestAsync_WhenCalled_ReturnsBadRequestResult()
  {
    // Arrange
    var mediator = new Mock<IMediator>();
    var controller = new SearchRequestController(mediator.Object);
    var request = new CreateSearchRequestCommand()
    {
      Keywords = "",
      Url = "",
      Engine = ""
    };

    var cancellationToken = new CancellationToken();
    var expectedResult = new BaseResponse<SearchRequestViewModel>()
    {
      Succcess = false,
      Message = "Create Search Request Command failed!",
      Errors = new List<BaseError>() {
        new BaseError { ErrorMessage = "Required", PropertyMessage = "Keywords" } ,
        new BaseError { ErrorMessage = "Required", PropertyMessage = "Url" } ,
        new BaseError { ErrorMessage = "Required", PropertyMessage = "Engine" } ,
      }
    };

    mediator.Setup(m => m.Send(request, cancellationToken)).ReturnsAsync(expectedResult);

    // Act
    var actionResult = controller.CreateSearchRequestAsync(request, cancellationToken)?.Result;

    // Assert
    actionResult.Should().NotBeNull();
    actionResult.Should().BeOfType<BadRequestObjectResult>();

    var badRequestResult = actionResult as BadRequestObjectResult;
    badRequestResult?.StatusCode.Should().Be(400);
    badRequestResult?.Value.Should().BeEquivalentTo(expectedResult);
  }
}
