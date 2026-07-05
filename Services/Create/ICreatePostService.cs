using cStudy.Dtos.Requests;
using cStudy.Dtos.Responses;

namespace cStudy.Services.Create;

public interface ICreatePostService
{
    Task<PostResponse> CreatePostAsync(CreatePostRequest request);
}
