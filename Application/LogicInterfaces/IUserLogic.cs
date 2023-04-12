using Shared.DTO;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDTO dto);
    public Task<IEnumerable<User>> GetAsync(SearchUserDto searchUser);
}