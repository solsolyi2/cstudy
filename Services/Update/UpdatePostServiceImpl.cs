using cStudy.Dao;
using cStudy.Dtos.Requests;
using cStudy.Dtos.Responses;
using cStudy.Services.Mapping;

namespace cStudy.Services.Update;

public sealed class UpdatePostServiceImpl(IPostDao postDao) : IUpdatePostService
{
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

        return PostMapper.ToResponse(post);
    }
}
