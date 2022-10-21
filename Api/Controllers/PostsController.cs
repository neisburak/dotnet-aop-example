using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
        return Ok(await _postService.GetAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _postService.GetAsync());
    }
}