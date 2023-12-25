using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using APIBasics.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserApi.Dtos;
using UserApi.Models;

namespace UserApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly DataContextDappar _dapper;
        private readonly IConfiguration _config;


        [ActivatorUtilitiesConstructor]
        public PostsController(ILogger<PostsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            _dapper = new DataContextDappar(configuration);
        }

        [HttpGet("getPosts", Name = "GetPots")]
        public ActionResult<IEnumerable<Post>> FetchPosts()
        {
            string sqlGetPosts = @$"EXEC TutorialAppSchema.spPost_Get";
            IEnumerable<Post> postList = _dapper.LoadData<Post>(sqlGetPosts);
            return Ok(postList);
        }

        [HttpGet("getPost/{postId}", Name = "GetPostById")]
        public ActionResult<Post> fetchPost(int postId)
        {
            string sqlGetPost = @$"EXEC TutorialAppSchema.spPost_Get @postId = {postId} ";
            Post queriePost = _dapper.LoadDataSingle<Post>(sqlGetPost);
            if (queriePost.PostId > 0)
            {
                return Ok(queriePost);
            }
            return NotFound($"Can not find post with id: {postId}");
        }

        [HttpGet("myPosts", Name = "GetPostByUserId")]
        public ActionResult<IEnumerable<Post>> GetPostByUserId()
        {
            int userId = int.Parse(this.User.FindFirst("userId")?.Value);

            string sqlGetPost = $@"EXEC TutorialAppSchema.spPost_Get @userId = {userId}";

            IEnumerable<Post> userPostList = _dapper.LoadData<Post>(sqlGetPost);
            if (userPostList.Count() > 0)
            {
                return Ok(userPostList);
            }
            return NotFound("Account has no Posts!");
        }

        [HttpPost("createPost", Name = "CreateNewPost")]
        public ActionResult CreatePost([FromBody] CreatePostDto newPost)
        {
            int userId = int.Parse(this.User.FindFirst("userId").Value);
            string sqlAddPost = @$"INSERT INTO TutorialAppSchema.Posts(
                [UserId],[PostTitle],[PostContent],[PostCreationDate],[LastUpdateddate]
            )VALUES ({userId}, '{newPost.PostTitle}', '{newPost.PostContent}',
                '{DateTime.Now}', '{DateTime.Now}')";

            bool postAdded = _dapper.ExecuteSql(sqlAddPost);
            if (postAdded)
            {
                return Ok("Post Added!");
            }
            return StatusCode(500, "Erro adding Post. Please try again!");
        }

        [HttpPut("editpost/{postId}", Name = "UpdatePost")]
        public ActionResult EditPost([FromBody] EditPostDto updatedPost, int postId)
        {
            int userId = int.Parse(this.User.FindFirst("userId").Value);

            string sqlEditPost = @$"UPDATE TutorialAppSchema.Posts
                SET [PostTitle] = '{updatedPost.PostTitle}', [PostContent] = '{updatedPost.PostContent}',
                [LastUpdateddate] = '{DateTime.Now}' WHERE [PostId] = {postId}
                AND [UserId] = {userId}";

            bool postUpdated = _dapper.ExecuteSql(sqlEditPost);
            if (postUpdated)
            {
                return Ok("Post updated!");
            }
            return StatusCode(500, "Error updating post. Please try again!");
        }

        [HttpDelete("deletePost/{postId}", Name = "DeletePost")]
        public ActionResult RemovePost(int postId)
        {
            int userId = int.Parse(this.User.FindFirst("userId").Value);
            string sqlDelete = @$"EXEC TutorialAppSchema.spPost_Delete 
                @postId = {postId}, @userId = {userId}";
            bool postDeleted = _dapper.ExecuteSql(sqlDelete);
            if (postDeleted)
            {
                return Ok("Post deleted");
            }
            return StatusCode(500, "Error deleteing post. Please try again!");
        }
    }
}