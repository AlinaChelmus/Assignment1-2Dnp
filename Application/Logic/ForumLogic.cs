using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTO;
using Shared.Models;

namespace Application.Logic;

public class ForumLogic: IForumLogic

{
    private readonly IForumDao ForumDao;
    private readonly IUserDao UserDao;

    public ForumLogic(IForumDao forumDao, IUserDao userDao)
    {
        ForumDao = forumDao;
        UserDao = userDao;
    }
    public async Task<Forum> CreateAsync(ForumCreationDTO dto)
    {
        User? user = await UserDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        ValidateForum(dto);
        Forum forum = new Forum(user, dto.Title, dto.ArticleContent);
        Forum created = await ForumDao.CreateAsync(forum);
        return created;
    }

    public Task<IEnumerable<Forum>> GetAsync(SearchArticleDto searchArticleDto)
    {
        return ForumDao.GetAsync(searchArticleDto);
    }

    public async Task UpdateAsync(ForumUpdateDto dto)
    {
        Forum? existing = await ForumDao.GetByIdAsync(dto.Id);
        if (existing == null)
        {
            throw new Exception($"Forum with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await UserDao.GetByIdAsync((int) dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }
        
        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        string contentToUse = dto.ArticleContent ?? existing.ArticleContent;

        Forum updated = new(userToUse, titleToUse, contentToUse);
        {
            updated.Title = titleToUse;
            updated.ArticleContent = contentToUse;
            updated.Id = existing.Id;
        }

       
        await ForumDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Forum? forum = await ForumDao.GetByIdAsync(id);
        if (forum == null)
        {
            throw new Exception($"Article with ID {id} was not found!");
        }

        await ForumDao.DeleteAsync(id);
    }


    private void ValidateForum(ForumCreationDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.ArticleContent)) throw new Exception("The article must have a content.");
    }
}