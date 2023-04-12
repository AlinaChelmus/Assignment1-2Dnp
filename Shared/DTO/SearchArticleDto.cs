namespace Shared.DTO;

public class SearchArticleDto
{
    //
    public string? userName { get; }
    public int? UserId { get; }
    public string? TitleContains { get; }

    public SearchArticleDto(string? username, int? userId, string? titleContains)
    {
        userName = username;
        UserId = userId;
        TitleContains = titleContains;
    }
}