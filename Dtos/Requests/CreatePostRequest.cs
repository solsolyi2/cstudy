namespace cStudy.Dtos.Requests;

public sealed record CreatePostRequest(
    string Title,
    string Content,
    string Author);
