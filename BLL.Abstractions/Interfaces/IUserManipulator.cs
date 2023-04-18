using Core.Abstractions.DTOs.Interfaces;
using Core.DTOs.Jwt;

namespace BLL.Abstractions.Interfaces
{
    public interface IUserManipulator<T>
    {
        Task<IUserDTO> RegisterNewUser(RegisterUser userModel);
        Task<IUserDTO> LoginUser(LoginUser loginUser);
    }
}