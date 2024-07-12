using FluentAssertions;
using InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
using NUnit.Framework;

namespace InfoTrack.SearchSiteInfo.UnitTests.UseCases.Search.Create;
public class CreateSearchRequestValidatorTests
{
  private readonly CreateSearchRequestValidator _sut = new();
  private readonly string KEYWORDS = "land registry searches";
  private readonly string URL = "www.infotrack.co.uk";
  private readonly string ENGINE = "Google";


  [TestCase(null)]
  [TestCase("")]
  [TestCase(" ")]
  public void Validate_Fails_If_Keywords_Is_Null(string keywords)
  {
    // Arrange
    var request = CreateRequest(keywords: keywords, url: URL, engine: ENGINE);

    // Act
    var result = _sut.Validate(request);

    // Assert
    result.IsValid.Should().BeFalse();
  }

  [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
  public void Validate_Fails_If_Keywords_Is_To_Long(string keywords)
  {
    // Arrange
    var request = CreateRequest(keywords: keywords, url: URL, engine: ENGINE);

    // Act
    var result = _sut.Validate(request);

    // Assert
    result.IsValid.Should().BeFalse();
  }

  [TestCase(null)]
  [TestCase("")]
  [TestCase(" ")]
  public void Validate_Fails_If_Url_Is_Null(string url)
  {
    // Arrange
    var request = CreateRequest(keywords: KEYWORDS, url: url, engine: ENGINE);

    // Act
    var result = _sut.Validate(request);

    // Assert
    result.IsValid.Should().BeFalse();
  }

  [Test]
  public void Validate_Fails_If_Url_Is_Incorrect_With_Message()
  {
    // Arrange
    var request = CreateRequest(keywords: KEYWORDS, url: "https // www.infotrack.co.uk ", engine: ENGINE);

    // Act
    var result = _sut.Validate(request);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be("Incorrect Url");
  }

  [TestCase(null)]
  [TestCase("")]
  [TestCase(" ")]
  public void Validate_Fails_If_Engine_Is_Null(string engine)
  {
    // Arrange
    var request = CreateRequest(keywords: KEYWORDS, url: URL, engine: engine);

    // Act
    var result = _sut.Validate(request);

    // Assert
    result.IsValid.Should().BeFalse();
  }

  [Test]
  public void Validate_Fails_If_Engine_Is_Incorrect_With_Message()
  {
    // Arrange
    var request = CreateRequest(keywords: KEYWORDS, url: URL, engine: "engine");

    // Act
    var result = _sut.Validate(request);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be("Incorrect Engine");
  }

  [Test]
  public void Validate_True_If_All_Params_Is_Correct()
  {
    // Arrange
    var request = CreateRequest(keywords: KEYWORDS, url: URL, engine: ENGINE);

    // Act
    var result = _sut.Validate(request);

    // Assert
    result.IsValid.Should().BeTrue();
  }

  private static CreateSearchRequestCommand CreateRequest(string keywords, string url, string engine)
  {
    var request =
        new CreateSearchRequestCommand
        {
          Keywords = keywords,
          Url = url,
          Engine = engine,
        };
    return request;
  }
}
