using cStudy.Dtos.Responses;
using cStudy.Models;

namespace cStudy.Services.Mapping;

public static class PostMapper
{
    public static PostResponse ToResponse(Post post)
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
