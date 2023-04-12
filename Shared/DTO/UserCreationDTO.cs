namespace Shared.DTO;

public class UserCreationDTO
{
    public string UserName { get;}
    public string Password { get; }

    public UserCreationDTO(string userName, string passWord)
    {
        UserName = userName;
        Password = passWord;

    }
}