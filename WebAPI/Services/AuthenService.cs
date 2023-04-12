using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using FileData;
using Shared.Models;

namespace WebAPI.Services;

public class AuthenService: IAuthenService
{
    
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
    };

    Task<User> IAuthenService.ValidateUser(string userName, string Password)
    {
        User? existinguser = _Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        
        if (existinguser == null)
        {
            throw new Exception("User not found");
        }

        if (!existinguser.Password.Equals(Password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existinguser);
    }

    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        
        users.Add(user);

        return Task.CompletedTask;
    }
}