using AutoMapper;
using Core.DB;
using Core.DTOs.Anime;
using Core.DTOs.Others;
using Core.Enums;

namespace AnimePortalAuthServer.Mapper
{
    public class AnimeMapperProfile : Profile
    {
        public AnimeMapperProfile()
        {
            CreateMap<Anime, AnimePreview>()
                .ForMember(dest => dest.Spotlight,
                    opt => opt.MapFrom(src => src.Photos!.FirstOrDefault(p => p.PhotoType == PhotoTypes.Spotlight)!.ImageUrl))
                .ForMember(dest=> dest.AnimeDescription, opt=> opt.MapFrom((src, dest, destMember, context) =>
                {
                    var desiredLanguage = context.Items["DesiredLanguage"].ToString();
                    return src.AnimeDescriptions.FirstOrDefault(lang => lang.Language.Name == desiredLanguage.ToLower());
                }))
                .ForMember(dest=> dest.Tags, opt=> opt.MapFrom(src=>src.Tags))
                .ReverseMap();

            CreateMap<Anime, AnimeDto>()
                .ForMember(dest => dest.AnimeDescription, opt => opt.MapFrom(src => src.AnimeDescriptions))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ReverseMap();

            CreateMap<AnimeDescription, AnimeDescriptionDto>()
                .ForMember(dest => dest.Language,
                    opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom(src => src.Genres))
                .ReverseMap();

            CreateMap<Language, LanguageDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Anime, AnimeDetailed>()
                .ForMember(dest => dest.Poster,
                    opt => opt.MapFrom(src =>
                        src.Photos!.FirstOrDefault(p => p.PhotoType == PhotoTypes.Previews)!.ImageUrl))
                .ForMember(dest => dest.Screenshots,
                    opt => opt.MapFrom(src => src.Photos.Where(p => p.PhotoType == PhotoTypes.Screenshots)
                        .ToList()))
                .ForMember(dest => dest.AnimeDescription,
                    opt => opt.MapFrom((src,
                        dest,
                        destMember,
                        context) =>
                {
                    var desiredLanguage = context.Items["DesiredLanguage"].ToString();
                    return src.AnimeDescriptions.FirstOrDefault(lang => lang.Language.Name == desiredLanguage.ToLower());
                })).ReverseMap();
            CreateMap<Photo, PhotoDto>().ForMember(dest=> dest.PhotoId, opt=> opt.MapFrom(src=>src.Id)).ReverseMap();
        }
    }
}
