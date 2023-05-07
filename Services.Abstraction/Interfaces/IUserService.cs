using Core.Abstractions.DTOs.Interfaces;
using Core.DTOs.Jwt;

namespace Services.Abstraction.Interfaces
{
    public interface IUserService<TUserDto, TRegisterUser, TLoginUser, TRefreshUser>
    where TUserDto : IUserDto
    {
        Task<TUserDto> RegisterNewUser(TRegisterUser userModel);
        Task<TUserDto> LoginUser(TLoginUser loginUser);
        Task<TUserDto> RefreshUser(TRefreshUser refreshUser);
    }
}