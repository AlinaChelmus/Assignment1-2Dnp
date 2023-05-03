using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTO;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class ForumEfcDao: IForumDao
{
    private readonly ForumContext context;

    public ForumEfcDao(ForumContext context)
    {
        this.context = context;
    }
    public async Task<Forum> CreateAsync(Forum forum)
    {
        EntityEntry<Forum> newForum = await context.Forums.AddAsync(forum);
        await context.SaveChangesAsync();
        return newForum.Entity;
    }

    public Task<IEnumerable<Forum>> GetAsync(SearchArticleDto searchArticle)
    {
        throw new NotImplementedException();
    }

    public async Task<Forum?> GetByIdAsync(int id)
    {
        Forum? existing = await context.Forums
            .Include(forum => forum.Id)
            .FirstOrDefaultAsync(forum => forum.Id == id);
        return existing;
    }

    public async Task UpdateAsync(Forum updateForum)
    {
        context.Forums.Update(updateForum);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Forum? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Article with id {id} not found");
        }

        context.Forums.Remove(existing);
        await context.SaveChangesAsync();
    }
}