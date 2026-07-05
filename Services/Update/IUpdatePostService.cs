using cStudy.Dtos.Requests;
using cStudy.Dtos.Responses;

namespace cStudy.Services.Update;

public interface IUpdatePostService
{
    Task<PostResponse?> UpdatePostAsync(int id, UpdatePostRequest request);
}
