using cStudy.Data;
using cStudy.Dao;
using cStudy.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPostDao, PostDao>();
builder.Services.AddScoped<IPostService, PostServiceImpl>();
builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
