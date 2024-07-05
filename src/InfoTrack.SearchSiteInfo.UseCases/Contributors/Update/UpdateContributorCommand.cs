using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InfoTrack.SearchSiteInfo.UseCases.Contributors.Update;

public record UpdateContributorCommand(int ContributorId, string NewName) : ICommand<Result<ContributorDTO>>;
