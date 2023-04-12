using Application.DaoInterfaces;
using Shared.DTO;
using Shared.Models;

namespace FileData.DAO;

public class ForumFileDao: IForumDao
{
    private readonly FileContext context;

    public ForumFileDao(FileContext context)
    {
        this.context = context;
    }
    public Task<Forum> CreateAsync(Forum forum)
    {
        int id = 1;
        if (context.Forums.Any())
        {
            id = context.Forums.Max(t => t.Id);
            id++;
        }

        forum.Id = id;
        context.Forums.Add(forum);
        context.SaveChanges();
        return Task.FromResult(forum);
    }

    public Task<IEnumerable<Forum>> GetAsync(SearchArticleDto searchArticle)
    {
        IEnumerable<Forum> result = context.Forums.AsEnumerable();
        if (!string.IsNullOrEmpty(searchArticle.userName))
        {
            result = context.Forums.Where(forum =>
                forum.Owner.UserName.Equals(searchArticle.userName, StringComparison.OrdinalIgnoreCase));
        }

        if (searchArticle.UserId!=null)
        {
            result = result.Where((t => t.Owner.Id == searchArticle.UserId));
        }

        if (!string.IsNullOrEmpty(searchArticle.TitleContains))
        {
            result = result.Where(t => t.Title.Contains(searchArticle.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task<Forum?> GetByIdAsync(int id)
    {
        Forum? existing = context.Forums.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(existing);
    }

    public Task UpdateAsync(Forum updateForum)
    {
        Forum? existing = context.Forums.FirstOrDefault(Forum => Forum.Id == updateForum.Id);
        if (existing == null)
        {
            throw new Exception($"Article with id {updateForum.Id} does not exist!");
        }

        context.Forums.Remove(existing);
        context.Forums.Add(updateForum);
        context.SaveChanges();

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Forum? existing = context.Forums.FirstOrDefault(Forum => Forum.Id == id);
        if (existing==null)
        {
            throw new Exception($"Article with id {id} does not exist!");
        }

        context.Forums.Remove(existing);
        context.SaveChanges();
        return Task.CompletedTask;
    }
}