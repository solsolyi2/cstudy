using cStudy.Dao;
using cStudy.Dtos.Requests;
using cStudy.Dtos.Responses;
using cStudy.Models;

namespace cStudy.Services;

public sealed class PostServiceImpl(IPostDao postDao) : IPostService
{
    public async Task<IReadOnlyList<PostResponse>> GetPostsAsync()
    {
        var posts = await postDao.GetAllAsync();
        return posts.Select(ToResponse).ToList();
    }

    public async Task<PostResponse?> GetPostAsync(int id)
    {
        var post = await postDao.GetByIdAsync(id);
        return post is null ? null : ToResponse(post);
    }

    public async Task<PostResponse> CreatePostAsync(CreatePostRequest request)
    {
        var now = DateTimeOffset.UtcNow;
        var post = new Post
        {
            Title = request.Title.Trim(),
            Content = request.Content.Trim(),
            Author = request.Author.Trim(),
            CreatedAt = now,
            UpdatedAt = now
        };

        await postDao.CreateAsync(post);

        return ToResponse(post);
    }

    public async Task<PostResponse?> UpdatePostAsync(int id, UpdatePostRequest request)
    {
        var post = await postDao.GetByIdAsync(id);
        if (post is null)
        {
            return null;
        }

        post.Title = request.Title.Trim();
        post.Content = request.Content.Trim();
        post.Author = request.Author.Trim();
        post.UpdatedAt = DateTimeOffset.UtcNow;

        await postDao.UpdateAsync(post);

        return ToResponse(post);
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        return await postDao.DeleteAsync(id);
    }

    private static PostResponse ToResponse(Post post)
    {
        return new PostResponse(
            post.Id,
            post.Title,
            post.Content,
            post.Author,
            post.CreatedAt,
            post.UpdatedAt);
    }
}
