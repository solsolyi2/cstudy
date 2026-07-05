namespace cStudy.Dtos;

public sealed record UpdatePostRequest(
    string Title,
    string Content,
    string Author);
