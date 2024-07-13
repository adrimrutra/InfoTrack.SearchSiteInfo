using System.Globalization;
using Bogus;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Infrastructure.Data;

namespace InfoTrack.SearchSiteInfo.KernelSetupTests;

public static class DatabaseSetup
{
  public static void SeedData(AppDbContext context)
  {
    context.SearchRequests.RemoveRange(context.SearchRequests);

    var rank = 1;
    var fakeSearchRequests = new Faker<SearchRequest>()
        .RuleFor(o => o.Url, f => "www.infotrack.co.uk")
        .RuleFor(o => o.Engine, f => Engine.GOOGLE)
        .RuleFor(o => o.Keywords, f => "land registry searches")
        .RuleFor(o => o.Rank, f => rank++)
        .RuleFor(o => o.CreatedDate, f => DateTimeOffset.Parse("01/01/2023 12:00", CultureInfo.CurrentCulture));

    var searchRequests = fakeSearchRequests.Generate(5);

    context.AddRange(searchRequests);

    context.SaveChanges();
  }
}
