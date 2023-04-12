using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTO;
using Shared.Models;


namespace Application.Logic;

public class UserLogic: IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync (UserCreationDTO dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName,
            Password = dto.Password
        };
    
        User created = await userDao.CreateAsync(toCreate);
    
        return created;
    }
  
    public Task<IEnumerable<User>> GetAsync(SearchUserDto searchUser)
    {
        return userDao.GetAsync(searchUser);
    }
    
    
    private static void ValidateData(UserCreationDTO userToCreate)
    {
        string userName = userToCreate.UserName;
        string passWord = userToCreate.Password;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        
        if (passWord.Length<8)
        {
            throw new Exception("Password must be at least 8 characters!");
        }

        if (passWord.Length>16)
        {
            throw new Exception("Password must be at most 15 characters!");
        }
    }
}