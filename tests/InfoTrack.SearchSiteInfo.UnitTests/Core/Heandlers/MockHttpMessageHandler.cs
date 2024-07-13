using System.Net;
using System.Net.Http.Headers;
using Moq;
using Moq.Protected;

namespace InfoTrack.SearchSiteInfo.UnitTests.Core.Heandlers;
public class MockHttpMessageHandler
{
  private readonly Mock<HttpMessageHandler> _mockHandler = new(MockBehavior.Strict);

  public HttpMessageHandler Object { get { return _mockHandler.Object; } }

  public MockHttpMessageHandler()
  {
    _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK))
                .Verifiable();

  }

  public Mock<HttpMessageHandler> ReturnsResponse(HttpContent content, string mediaType, HttpStatusCode statusCode)
  {
    _mockHandler.Protected()
        .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        )
        .ReturnsAsync(new HttpResponseMessage
        {
          StatusCode = statusCode,
          Content = content
        })
        .Callback<HttpRequestMessage, CancellationToken>((req, ct) => req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType)))
        .Verifiable();

    return _mockHandler;
  }
}
