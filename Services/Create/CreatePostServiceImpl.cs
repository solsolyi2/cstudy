using cStudy.Dao;
using cStudy.Dtos.Requests;
using cStudy.Dtos.Responses;
using cStudy.Models;
using cStudy.Services.Mapping;

namespace cStudy.Services.Create;

public sealed class CreatePostServiceImpl(IPostDao postDao) : ICreatePostService
{
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

        return PostMapper.ToResponse(post);
    }
}
