using Shared.DTO;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IForumLogic
{
    Task<Forum> CreateAsync(ForumCreationDTO dto);

    Task<IEnumerable<Forum>> GetAsync(SearchArticleDto searchArticleDto);

    Task UpdateAsync(ForumUpdateDto dto);

    Task DeleteAsync(int id);
}