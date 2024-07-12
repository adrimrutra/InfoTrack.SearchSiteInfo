using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InfoTrack.SearchSiteInfo.Core.Entities;

namespace InfoTrack.SearchSiteInfo.Infrastructure.Data.Config;
public class SearchRequestConfiguration : IEntityTypeConfiguration<SearchRequest>
{
  public void Configure(EntityTypeBuilder<SearchRequest> builder)
  {
    builder.Property(p => p.Id).ValueGeneratedOnAdd();
    builder.Property(p => p.Keywords).IsRequired();
    builder.Property(p => p.Url).IsRequired();
    builder.Property(p => p.Engine).IsRequired();
    builder.Property(p => p.Rank).IsRequired();
    builder.Property(p => p.CreatedDate).IsRequired();
  }
}
