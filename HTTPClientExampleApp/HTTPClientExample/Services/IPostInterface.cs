using HTTPClientExample.Models.Requests;
using HTTPClientExample.Models.Responses;
using System.Collections.Generic;

namespace HTTPClientExample.Services
{
    public interface IPostInterface
    {
        #region Post methods definations
        Task<ServiceResponse<List<GetPostResponse>>> GetPosts();
        Task<ServiceResponse<GetPostResponse>> GetPostById(int postId);
        Task<ServiceResponse<CreatePostResponse>> CreatePost(PostRequest post);
        Task<ServiceResponse<CreatePostResponse>> UpdatePost(PostRequest post, int Id);
        Task<ServiceResponse<CreatePostResponse>> PatchPost(PostRequest post);
        Task<ServiceResponse<string>> DeletePost(int postId);
        Task<ServiceResponse<List<GetPostResponse>>> FilterPost(int userId);
        Task<ServiceResponse<List<NestedPostResponse>>> GetPostComments(int postId);
        #endregion

    }
}
