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
            await _unitOfWork.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            newUser.PasswordHash = _hasher.Hash(newUser.PasswordHash);

            await _unitOfWork.UserRepository.CreateAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            return newUser;
        }

        public async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            IEnumerable<User> usersThatFit = await _unitOfWork.UserRepository.ReadByConditionAsync(predicate);
            return usersThatFit.FirstOrDefault();
        }

        public async Task<User?> GetUserByCredentialsAsync(string identifier, string password)
        {
            string hashedPassword = _hasher.Hash(password);
            IEnumerable<User> usersWithSameCredentials = await _unitOfWork.UserRepository.ReadByConditionAsync(user => (user.Name == identifier || user.Email == identifier) && user.PasswordHash == hashedPassword);
            return usersWithSameCredentials.FirstOrDefault();
        }

        public async Task UpdateUserAsync(User newValuesForUser)
        {
            if (newValuesForUser.Id < 1)
            {
                await CreateUserAsync(newValuesForUser);
            }
            else
            {
                await _unitOfWork.UserRepository.UpdateAsync(newValuesForUser);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            await _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}