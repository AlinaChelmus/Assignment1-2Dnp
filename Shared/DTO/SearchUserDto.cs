namespace Shared.DTO;

public class SearchUserDto
{
    public string? UsernameContains { get; }


    public SearchUserDto(string? usernameContains)
    {
        UsernameContains = usernameContains;

    }
}