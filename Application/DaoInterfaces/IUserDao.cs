
using Shared.DTO;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    Task<User?> GetByIdAsync(int dtoOwnerId);
    Task<IEnumerable<User>> GetAsync(SearchUserDto searchUser);
}