namespace cStudy.Dtos.Requests;

public sealed record UpdatePostRequest(
    string Title,
    string Content,
    string Author);
