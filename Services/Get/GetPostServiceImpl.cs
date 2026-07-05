using cStudy.Dao;
using cStudy.Dtos.Responses;
using cStudy.Services.Mapping;

namespace cStudy.Services.Get;

public sealed class GetPostServiceImpl(IPostDao postDao) : IGetPostService
{
    public async Task<IReadOnlyList<PostResponse>> GetPostsAsync()
    {
        var posts = await postDao.GetAllAsync();
        return posts.Select(PostMapper.ToResponse).ToList();
    }

    public async Task<PostResponse?> GetPostAsync(int id)
    {
        var post = await postDao.GetByIdAsync(id);
        return post is null ? null : PostMapper.ToResponse(post);
    }
}
