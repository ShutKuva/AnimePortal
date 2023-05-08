using Core.Abstractions.DTOs.Interfaces;
using Core.DTOs.Jwt;

namespace Services.Abstraction.Interfaces
{
    public interface IUserService<TUserDto, TRegisterUser, TLoginUser, TRefreshUser>
    where TUserDto : IUserDto
    {
        Task<TUserDto> RegisterNewUserAsync(TRegisterUser userModel);
        Task<TUserDto> LoginUserAsync(TLoginUser loginUser);
        Task<TUserDto> RefreshUserAsync(TRefreshUser refreshUser);
    }
}