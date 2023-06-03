using AutoMapper;
using Core.DB;
using Core.DTOs.Others;

namespace AnimePortalAuthServer.Mapper
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
