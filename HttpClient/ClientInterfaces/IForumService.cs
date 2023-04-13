using Shared.DTO;
using Shared.Models;

namespace HTTPClient.ClientInterfaces;

public interface IForumService
{
    Task CreateAsync(ForumCreationDTO dto);
    Task<IEnumerable<Forum>?> GetArticles(string? articleContent= null);

    Task DeleteAsync(int articleId);
}