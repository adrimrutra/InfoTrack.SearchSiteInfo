using InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfoTrack.SearchSiteInfo.WebApi.Controllers;
[Route("api/search")]
[ApiController]
public class SearchRequestController(IMediator _mediator) : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> CreateSearchRequestAsync(
    [FromBody] CreateSearchRequestCommand request, CancellationToken cancellationToken)
  {
    var response = await _mediator.Send(request, cancellationToken);

    if (response.Succcess)
    {
      return Ok(response);
    }

    return BadRequest(response);
  }
}
