using cStudy.Dtos.Requests;
using cStudy.Dtos.Responses;
using cStudy.Services.Create;
using cStudy.Services.Delete;
using cStudy.Services.Get;
using cStudy.Services.Update;
using Microsoft.AspNetCore.Mvc;

namespace cStudy.Controllers;

[ApiController]
[Route("api/posts")]
public sealed class PostsController(
    ICreatePostService createPostService,
    IGetPostService getPostService,
    IUpdatePostService updatePostService,
    IDeletePostService deletePostService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PostResponse>>> GetPosts()
    {
        var posts = await getPostService.GetPostsAsync();
        return Ok(posts);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostResponse>> GetPost(int id)
    {
        var post = await getPostService.GetPostAsync(id);
        if (post is null)
        {
            return NotFound(new { message = "Post not found." });
        }

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<PostResponse>> CreatePost(CreatePostRequest request)
    {
        if (!IsValidRequest(request.Title, request.Content, request.Author))
        {
            return BadRequest(new { message = "Title, content, and author are required." });
        }

        var post = await createPostService.CreatePostAsync(request);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PostResponse>> UpdatePost(int id, UpdatePostRequest request)
    {
        if (!IsValidRequest(request.Title, request.Content, request.Author))
        {
            return BadRequest(new { message = "Title, content, and author are required." });
        }

        var post = await updatePostService.UpdatePostAsync(id, request);
        if (post is null)
        {
            return NotFound(new { message = "Post not found." });
        }

        return Ok(post);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var deleted = await deletePostService.DeletePostAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = "Post not found." });
        }

        return NoContent();
    }

    private static bool IsValidRequest(string title, string content, string author)
    {
        return !string.IsNullOrWhiteSpace(title) &&
               !string.IsNullOrWhiteSpace(content) &&
               !string.IsNullOrWhiteSpace(author);
    }
}
