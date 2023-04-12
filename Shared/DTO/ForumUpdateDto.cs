namespace Shared.DTO;

public class ForumUpdateDto
{
    public int Id { get; }
    public int? OwnerId { get; }
    public string? Title { get; }
    public string? ArticleContent { get; }

    public ForumUpdateDto(int id, int? ownerId, string? title, string? articleContent)
    {
        Id = id;
        OwnerId = ownerId;
        Title = title;
        ArticleContent = articleContent;
    }
}