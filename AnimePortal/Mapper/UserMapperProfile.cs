using AutoMapper;
using Core.DTOs.Jwt;

namespace AnimePortalAuthServer.Mapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserMapperProfile, JwtUserDTO>();
        }
    }
}
