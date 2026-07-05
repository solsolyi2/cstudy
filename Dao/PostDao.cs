using cStudy.Data;
using cStudy.Models;
using Microsoft.EntityFrameworkCore;

namespace cStudy.Dao;

public sealed class PostDao(AppDbContext db) : IPostDao
{
    public async Task<IReadOnlyList<Post>> GetAllAsync()
    {
        return await db.Posts
            .OrderByDescending(post => post.CreatedAt)
            .ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await db.Posts.FindAsync(id);
    }

    public async Task<Post> CreateAsync(Post post)
    {
        db.Posts.Add(post);
        await db.SaveChangesAsync();
        return post;
    }

    public async Task<Post?> UpdateAsync(Post post)
    {
        var exists = await db.Posts.AnyAsync(existingPost => existingPost.Id == post.Id);
        if (!exists)
        {
            return null;
        }

        db.Posts.Update(post);
        await db.SaveChangesAsync();
        return post;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var post = await db.Posts.FindAsync(id);
        if (post is null)
        {
            return false;
        }

        db.Posts.Remove(post);
        await db.SaveChangesAsync();
        return true;
    }
}
