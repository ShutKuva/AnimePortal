using AutoMapper;
using Core.DB;
using Core.DTOs.Anime;

namespace AnimePortalAuthServer.Mapper
{
    public class AnimeMapperProfile: Profile
    {
        public AnimeMapperProfile()
        {
            CreateMap<Anime, AnimePreview>().ReverseMap();
        }
    }
}
