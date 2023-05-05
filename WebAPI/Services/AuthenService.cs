
using Application.DaoInterfaces;
using Shared.Models;

namespace WebAPI.Services;

public class AuthenService: IAuthenService
{
    private IUserDao _userDao;

    public AuthenService(IUserDao userDao)
    {
        _userDao = userDao;
    }
    /*
    private const string filepath = "data.json";
    private DataContainer? dataContainer;
    
    
    
    private void LoadData()
    {
        if (dataContainer!=null) return;

        if (!File.Exists(filepath))
        {
            dataContainer = new()
            {
                Forums = new List<Forum>(),
                Users = new List<User>()
            };
            return;
        }

        string content = File.ReadAllText(filepath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }
    
    public IList<User> _Users
    {
        
        get
        {
            dataContainer = new()
            {
                Forums = new List<Forum>(),
                Users = new List<User>()
            };
            string content = File.ReadAllText(filepath);
            dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
            return (IList<User>)dataContainer.Users;
        }
    }
    
    private  IList<User> users = new List<User>
    {
        new User
        {
            Id = 1,
            Password = "abcdefgh",
            Role = "Admin",
            UserName = "alina"
        },
        new User
        {
            Id = 2,
            Password = "aaaaaaaa",
            Role = "user",
            UserName = "sorina"
        }
    };*/

    public async Task<User> ValidateUser(string userName, string Password)
    {
        User? exist = await _userDao.GetByUsernameAsync(userName);
       
        if (exist == null)
        {
            throw new Exception("User not found");
        }

        if (!exist.Password.Equals(Password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(exist);
    }
    }