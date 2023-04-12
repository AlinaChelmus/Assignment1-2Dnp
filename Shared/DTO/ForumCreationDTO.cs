namespace Shared.DTO;

public class ForumCreationDTO
{
    public int OwnerId { get; }
    public string Title { get; }
   public string ArticleContent { get; }
    
    public ForumCreationDTO(int ownerId, string title, string articleContent)
    {
        OwnerId = ownerId;
        Title = title;
        ArticleContent = articleContent;
    }
}