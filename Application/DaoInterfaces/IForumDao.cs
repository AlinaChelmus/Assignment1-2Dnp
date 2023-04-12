using Shared.DTO;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IForumDao
{
    Task<Forum> CreateAsync(Forum forum);

    Task<IEnumerable<Forum>> GetAsync(SearchArticleDto searchArticle);

    Task<Forum?> GetByIdAsync(int id);
    Task UpdateAsync(Forum updateForum);

    Task DeleteAsync(int id);
}