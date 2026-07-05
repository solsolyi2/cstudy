using cStudy.Data;
using cStudy.Dtos;
using cStudy.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/", () => Results.Ok(new
{
    service = "cStudy board API",
    endpoints = new[]
    {
        "GET /api/posts",
        "GET /api/posts/{id}",
        "POST /api/posts",
        "PUT /api/posts/{id}",
        "DELETE /api/posts/{id}"
    }
}));

app.MapGet("/api/posts", async (AppDbContext db) =>
{
    var posts = await db.Posts
        .OrderByDescending(post => post.CreatedAt)
        .Select(post => new PostResponse(
            post.Id,
            post.Title,
            post.Content,
            post.Author,
            post.CreatedAt,
            post.UpdatedAt))
        .ToListAsync();

    return Results.Ok(posts);
});

app.MapGet("/api/posts/{id:int}", async (int id, AppDbContext db) =>
{
    var post = await db.Posts.FindAsync(id);

    return post is null
        ? Results.NotFound(new { message = "Post not found." })
        : Results.Ok(new PostResponse(
            post.Id,
            post.Title,
            post.Content,
            post.Author,
            post.CreatedAt,
            post.UpdatedAt));
});

app.MapPost("/api/posts", async (CreatePostRequest request, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(request.Title) ||
        string.IsNullOrWhiteSpace(request.Content) ||
        string.IsNullOrWhiteSpace(request.Author))
    {
        return Results.BadRequest(new { message = "Title, content, and author are required." });
    }

    var now = DateTimeOffset.UtcNow;
    var post = new Post
    {
        Title = request.Title.Trim(),
        Content = request.Content.Trim(),
        Author = request.Author.Trim(),
        CreatedAt = now,
        UpdatedAt = now
    };

    db.Posts.Add(post);
    await db.SaveChangesAsync();

    var response = new PostResponse(
        post.Id,
        post.Title,
        post.Content,
        post.Author,
        post.CreatedAt,
        post.UpdatedAt);

    return Results.Created($"/api/posts/{post.Id}", response);
});

app.MapPut("/api/posts/{id:int}", async (int id, UpdatePostRequest request, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(request.Title) ||
        string.IsNullOrWhiteSpace(request.Content) ||
        string.IsNullOrWhiteSpace(request.Author))
    {
        return Results.BadRequest(new { message = "Title, content, and author are required." });
    }

    var post = await db.Posts.FindAsync(id);
    if (post is null)
    {
        return Results.NotFound(new { message = "Post not found." });
    }

    post.Title = request.Title.Trim();
    post.Content = request.Content.Trim();
    post.Author = request.Author.Trim();
    post.UpdatedAt = DateTimeOffset.UtcNow;

    await db.SaveChangesAsync();

    return Results.Ok(new PostResponse(
        post.Id,
        post.Title,
        post.Content,
        post.Author,
        post.CreatedAt,
        post.UpdatedAt));
});

app.MapDelete("/api/posts/{id:int}", async (int id, AppDbContext db) =>
{
    var post = await db.Posts.FindAsync(id);
    if (post is null)
    {
        return Results.NotFound(new { message = "Post not found." });
    }

    db.Posts.Remove(post);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
