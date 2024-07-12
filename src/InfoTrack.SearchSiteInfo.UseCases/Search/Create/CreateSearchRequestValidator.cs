using FluentValidation;
using InfoTrack.SearchSiteInfo.Core.Constants;

namespace InfoTrack.SearchSiteInfo.UseCases.Searech.Create;
public class CreateSearchRequestValidator : AbstractValidator<CreateSearchRequestCommand>
{
  public CreateSearchRequestValidator()
  {
    RuleFor(x => x.Keywords)
      .NotEmpty()
      .MaximumLength(100)
      .WithMessage("Maximum Length 100");

    RuleFor(x => x.Url)
      .NotEmpty()
      .Matches("^(?:(http|https)?:\\/\\/)?(?:[\\w-]+\\.)+([a-z]|[A-Z]|[0-9]){2,6}$")
      .WithMessage("Incorrect Url"); ;

    RuleFor(x => x.Engine)
      .NotEmpty()
      .Must(engine => engine == Engine.GOOGLE || engine == Engine.BING || engine == Engine.YAHOO)
      .WithMessage("Incorrect Engine");
  }
}
