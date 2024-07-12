using AutoMapper;
using InfoTrack.SearchSiteInfo.Core.Entities;
using InfoTrack.SearchSiteInfo.Core.Models;
using InfoTrack.SearchSiteInfo.UseCases.Searech.Create;

namespace InfoTrack.SearchSiteInfo.UseCases.Commons.Mappings;
public class CreateSearchMapper : Profile
{
  public CreateSearchMapper()
  {
    CreateMap<CreateSearchRequestCommand, SearchRequestDto>()
        .ForMember(dest => dest.Keywords, opt => opt.MapFrom(src => src.Keywords))
        .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
        .ForMember(dest => dest.Engine, opt => opt.MapFrom(src => src.Engine))
        .ForMember(dest => dest.Count, opt => opt.MapFrom(src => 100));

    CreateMap<SearchRequestDto, SearchRequest>()
      .ForMember(dest => dest.Keywords, opt => opt.MapFrom(src => src.Keywords))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
      .ForMember(dest => dest.Engine, opt => opt.MapFrom(src => src.Engine))
      .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeOffset.Now));

    CreateMap<SearchRequest, SearchRequestViewModel>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Keywords, opt => opt.MapFrom(src => src.Keywords))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
      .ForMember(dest => dest.Engine, opt => opt.MapFrom(src => src.Engine))
      .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("s")));
  }
}
