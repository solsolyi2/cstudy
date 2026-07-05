using cStudy.Dtos.Requests;
using cStudy.Dtos.Responses;

namespace cStudy.Services;

public interface IPostService
{
    Task<IReadOnlyList<PostResponse>> GetPostsAsync();
    Task<PostResponse?> GetPostAsync(int id);
    Task<PostResponse> CreatePostAsync(CreatePostRequest request);
    Task<PostResponse?> UpdatePostAsync(int id, UpdatePostRequest request);
    Task<bool> DeletePostAsync(int id);
}
