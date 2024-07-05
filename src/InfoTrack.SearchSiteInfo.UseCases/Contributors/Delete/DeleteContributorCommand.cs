using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InfoTrack.SearchSiteInfo.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;
