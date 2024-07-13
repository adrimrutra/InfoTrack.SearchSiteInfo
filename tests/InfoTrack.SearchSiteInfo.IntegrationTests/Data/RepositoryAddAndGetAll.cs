using System.Globalization;
using FluentAssertions;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Entities;
using NUnit.Framework;

namespace InfoTrack.SearchSiteInfo.IntegrationTests.Data;

public class RepositoryAddAndGetAll : BaseUnitOfWorkTestFixture
{
  [Test]
  public async Task GetRepositoryAddsSearchRequestSetsIdAndGetAll()
  {
    var searchRequest = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 2, CreatedDate = DateTimeOffset.Parse("01/01/2023 12:00", CultureInfo.CurrentCulture) };

    var unitOfWork = GetUnitOfWork();
    var repository = unitOfWork.GetRepository<SearchRequest>();

    await repository.AddAsync(searchRequest);
    await unitOfWork.SaveChangesAsync();

    var result = (await repository.GetAllAsync())
                    .FirstOrDefault();

    result.Should().NotBeNull();
    result?.Url.Should().Be(searchRequest.Url);
    result?.Engine.Should().Be(searchRequest.Engine);
    result?.Keywords.Should().Be(searchRequest.Keywords);
    result?.Rank.Should().Be(searchRequest.Rank);
    result?.Id.Should().BePositive();
  }
}
