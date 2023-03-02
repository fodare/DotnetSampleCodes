using HTTPClientExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace HTTPClientExample.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostInterface _postService;

        public PostController(IPostInterface postService)
        {
            _postService = postService;
        }

        [HttpGet("GetPosts", Name = "GetPosts")]
        public async Task<ActionResult> GetPosts()
        {
            var res = await _postService.GetPosts();
            if (res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpGet("GetPostById/{id}", Name = "GetPostById")]
        public async Task<ActionResult> GetPostById(int id)
        {
            var res = await _postService.GetPostById(id);
            if (res.Success == false)
            {
                return BadRequest(res);
            } else
            {
                return Ok(res);
            }
        }
    }
}
