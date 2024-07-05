using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InfoTrack.SearchSiteInfo.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
