using AutoMapper;
using Core.DB;
using Core.DB.Videos;
using Core.DTOs.Anime.Videos;

namespace AnimePortalAuthServer.Mapper
{
    public class VideoMapperProfile : Profile
    {
        public VideoMapperProfile()
        { 
            CreateMap<Video, VideoDto>().ReverseMap();

            CreateMap<Player, PlayerDto>().ReverseMap();
        }
    }
}
