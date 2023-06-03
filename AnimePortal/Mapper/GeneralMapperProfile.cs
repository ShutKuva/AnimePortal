using AutoMapper;
using Core.DB;
using Core.DTOs.Others;

namespace AnimePortalAuthServer.Mapper
{
    public class GeneralMapperProfile : Profile
    {
        public GeneralMapperProfile()
        {
            CreateMap<Language, LanguageDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Tag, TagDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
