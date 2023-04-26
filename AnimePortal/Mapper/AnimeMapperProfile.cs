using AutoMapper;
using Core.DB;
using Core.DTOs.Anime;

namespace AnimePortalAuthServer.Mapper
{
    public class AnimeMapperProfile : Profile
    {
        public AnimeMapperProfile()
        {
            CreateMap<Anime, AnimePreview>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.Photos!.FirstOrDefault()!.ImageUrl))
                .ReverseMap();
            CreateMap<Anime, AnimeDto>().ReverseMap();
        }
    }
}
