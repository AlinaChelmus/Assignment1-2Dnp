using Shared.Models;

namespace WebAPI.Services;

public interface IAuthenService
{
    public Task<User> ValidateUser(string userName, string Password);
     
   
    
}