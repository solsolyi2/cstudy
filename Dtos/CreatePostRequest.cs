namespace cStudy.Dtos;

public sealed record CreatePostRequest(
    string Title,
    string Content,
    string Author);
