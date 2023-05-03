using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTO;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class UserEfcDao: IUserDao
{
    private readonly ForumContext context;
    public UserEfcDao(ForumContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        if (String.IsNullOrEmpty(userName))
        {
            throw new Exception("Username field cannot be null");
        }
        
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }

    public async Task<User?> GetByIdAsync(int dtoOwnerId)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Id == dtoOwnerId);
        return existing;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserDto searchUser)
    {
        var existingUsers = context.Users;
        IEnumerable<User> result = await existingUsers.ToListAsync();
        return result;
    }
}