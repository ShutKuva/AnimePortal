using Core.DB;
using System.Linq.Expressions;

namespace Services.Abstraction.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string name, string password, string email, string refreshToken = null!);
        Task<User> CreateUserAsync(User newUser);

        Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate);
        Task<User?> GetUserByCredentialsAsync(string identifier, string password);

        Task UpdateUserAsync(User newValuesForUser);

        Task DeleteUserAsync(int id);
    }
}