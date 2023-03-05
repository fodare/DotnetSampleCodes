using HTTPClientExample.Models.Requests;
using HTTPClientExample.Models.Responses;
using HTTPClientExample.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost("CreatePost", Name = "CreateNewPost")]
        public async Task<ActionResult> CreateNewPost(PostRequest newPost)
        {
            var res = await _postService.CreatePost(newPost);
            if (res.Success == false)
            {
                return BadRequest(res);
            }
            else
            {
                return CreatedAtAction("CreateNewPost", res);
            }
        }

        [HttpPut("UpdatePost/{id}", Name = ("UpdatePost"))]
        public async Task<ActionResult> UpdatePostById([FromBody] PostRequest post, int id)
        {
            var response = new ServiceResponse<string>();
            if (id != post.id)
            {
                response.Success = false;
                response.Message = "Post id not euqal to body.post.id";
                return BadRequest(response);
            }
            var res = await _postService.UpdatePost(post, id);
            if (res.Success == false)
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpDelete("DeletePost/{id}", Name = ("DeletePostByid"))]
        public async Task<ActionResult> DeletePostByid(int id)
        {
            var res = await _postService.DeletePost(id);
            if (res is null)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet("FilterPost/{id}", Name = ("FilterPostById"))]
        public async Task<ActionResult> FilterPostById(int id)
        {
            var res = await _postService.FilterPost(id);
            if (res.Success == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet("GetComment/{id}", Name = ("PostComments"))]
        public async Task<ActionResult> GetPostComment(int id)
        {
            var res = await _postService.GetPostComments(id);
            if (res.Success == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
