using cStudy.Dtos.Responses;

namespace cStudy.Services.Get;

public interface IGetPostService
{
    Task<IReadOnlyList<PostResponse>> GetPostsAsync();
    Task<PostResponse?> GetPostAsync(int id);
}
