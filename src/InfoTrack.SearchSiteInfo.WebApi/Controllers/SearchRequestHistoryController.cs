using InfoTrack.SearchSiteInfo.UseCases.History.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfoTrack.SearchSiteInfo.WebApi.Controllers;
[Route("api/History")]
[ApiController]
public class SearchRequestHistoryController(IMediator _mediator) : ControllerBase
{
  [HttpGet("GetAll")]
  public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(new GetSearchRequestHistoryQuery(), cancellationToken);
    if (response.Succcess)
    {
      return Ok(response);
    }

    return BadRequest(response);
  }
}
