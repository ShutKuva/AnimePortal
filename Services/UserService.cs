using BLL.Abstractions.Interfaces;
using Core.DB;
using Core.DTOs.Jwt;
using DAL.Abstractions.Interfaces;
using Services.Abstraction.Interfaces;
using System.Linq.Expressions;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHasher _hasher;

        public UserService(IUnitOfWork unitOfWork, IHasher hasher)
        {
            _unitOfWork = unitOfWork;
            _hasher = hasher;
        }

        public async Task<User> CreateUserAsync(string name, string password, string email, string refreshToken = null!)
        {
            User newUser = new User
            {
                Name = name,
                PasswordHash = _hasher.Hash(password),
                Email = email,
                RefreshToken = refreshToken,
            };

            await _unitOfWork.UserRepository.CreateAsync(newUser);

            return newUser;
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            newUser.PasswordHash = _hasher.Hash(newUser.PasswordHash);

            await _unitOfWork.UserRepository.CreateAsync(newUser);

            return newUser;
        }

        public async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            IEnumerable<User> usersThatFit = await _unitOfWork.UserRepository.ReadByConditionAsync(predicate);
            return usersThatFit.FirstOrDefault();
        }

        public async Task<User?> GetUserByCredentialsAsync(string identifier, string password)
        {
            IEnumerable<User> usersWithSameCredentials = await _unitOfWork.UserRepository.ReadByConditionAsync(user => (user.Name == identifier || user.Email == identifier) && user.PasswordHash == _hasher.Hash(password));
            return usersWithSameCredentials.FirstOrDefault();
        }
        public Task UpdateUserAsync(User newValuesForUser)
        {
            return _unitOfWork.UserRepository.UpdateAsync(newValuesForUser);
        }

        public Task DeleteUserAsync(int id)
        {
            return _unitOfWork.UserRepository.DeleteAsync(id);
        }
    }
}