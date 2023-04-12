using Application.DaoInterfaces;
using Shared.DTO;
using Shared.Models;

namespace FileData.DAO;

public class UserFileDao: IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }
    
    
    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;
        user.Password = user.Password;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<User?> GetByIdAsync(int dtoOwnerId)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == dtoOwnerId);
        return Task.FromResult(existing);
    }
    
    public Task<IEnumerable<User>> GetAsync(SearchUserDto searchUser)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchUser.UsernameContains!=null)
        {
            users = context.Users.Where(u =>
                u.UserName.Contains(searchUser.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }
    
}