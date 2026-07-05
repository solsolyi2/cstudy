namespace cStudy.Dtos;

public sealed record PostResponse(
    int Id,
    string Title,
    string Content,
    string Author,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);
