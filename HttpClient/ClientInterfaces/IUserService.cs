using Shared.Models;
using Shared.DTO;

namespace HTTPClient.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDTO dto);
    Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
    Task<User> ValidateUser(string userName, string Password);
    Task RegisterUser(User user);
}