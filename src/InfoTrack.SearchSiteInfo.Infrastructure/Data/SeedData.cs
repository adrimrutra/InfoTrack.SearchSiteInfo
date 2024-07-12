using System.Globalization;
using InfoTrack.SearchSiteInfo.Core.Constants;
using InfoTrack.SearchSiteInfo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.SearchSiteInfo.Infrastructure.Data;

public static class SeedData
{
  public static readonly SearchRequest searchRequest1 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 2, CreatedDate = DateTimeOffset.Parse("01/01/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest2 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.BING, Keywords = "land registry searches", Rank = 10, CreatedDate = DateTimeOffset.Parse("02/02/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest3 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.YAHOO, Keywords = "land registry searches", Rank = 18, CreatedDate = DateTimeOffset.Parse("03/03/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest4 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 3, CreatedDate = DateTimeOffset.Parse("04/04/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest5 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.YAHOO, Keywords = "land registry searches", Rank = 88, CreatedDate = DateTimeOffset.Parse("05/05/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest6 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 45, CreatedDate = DateTimeOffset.Parse("06/06/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest7 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.BING, Keywords = "land registry searches", Rank = 90, CreatedDate = DateTimeOffset.Parse("07/07/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest8 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 3, CreatedDate = DateTimeOffset.Parse("08/08/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest9 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.YAHOO, Keywords = "land registry searches", Rank = 8, CreatedDate = DateTimeOffset.Parse("09/09/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest10 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.BING, Keywords = "land registry searches", Rank = 45, CreatedDate = DateTimeOffset.Parse("10/10/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest11 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 73, CreatedDate = DateTimeOffset.Parse("11/11/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest12 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.YAHOO, Keywords = "land registry searches", Rank = 39, CreatedDate = DateTimeOffset.Parse("12/12/2023 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest13 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 55, CreatedDate = DateTimeOffset.Parse("01/01/2024 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest14 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.BING, Keywords = "land registry searches", Rank = 76, CreatedDate = DateTimeOffset.Parse("02/02/2024 12:00", CultureInfo.CurrentCulture) };
  public static readonly SearchRequest searchRequest15 = new SearchRequest() { Url = "www.infotrack.co.uk", Engine = Engine.GOOGLE, Keywords = "land registry searches", Rank = 34, CreatedDate = DateTimeOffset.Parse("03/03/2024 12:00", CultureInfo.CurrentCulture) };

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
    {
      if (dbContext.SearchRequests.Any()) return;

      PopulateTestData(dbContext);
    }
  }
  public static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var searchRequestt in dbContext.SearchRequests)
    {
      dbContext.Remove(searchRequestt);
    }
    dbContext.SaveChanges();

    dbContext.SearchRequests.Add(searchRequest1);
    dbContext.SearchRequests.Add(searchRequest2);
    dbContext.SearchRequests.Add(searchRequest3);
    dbContext.SearchRequests.Add(searchRequest4);
    dbContext.SearchRequests.Add(searchRequest5);
    dbContext.SearchRequests.Add(searchRequest6);
    dbContext.SearchRequests.Add(searchRequest7);
    dbContext.SearchRequests.Add(searchRequest8);
    dbContext.SearchRequests.Add(searchRequest9);
    dbContext.SearchRequests.Add(searchRequest10);
    dbContext.SearchRequests.Add(searchRequest11);
    dbContext.SearchRequests.Add(searchRequest12);
    dbContext.SearchRequests.Add(searchRequest13);
    dbContext.SearchRequests.Add(searchRequest14);
    dbContext.SearchRequests.Add(searchRequest15);

    dbContext.SaveChanges();
  }
}
