using Shared.Models;

namespace WebAPI.Services;

public interface IAuthenService
{
    Task<User> ValidateUser(string userName, string Password);
    Task RegisterUser(User user);
   
    
}