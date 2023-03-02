using HTTPClientExample.Models.Requests;
using HTTPClientExample.Models.Responses;
using static System.Net.WebRequestMethods;

namespace HTTPClientExample.Services
{
    public class PostService : IPostInterface
    {
        private readonly HttpClient _client;

        public PostService(HttpClient client)
        {
            _client = client;
        }

        public Task<ServiceResponse<GetPostResponse>> CreatePost(PostRequest post)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<string>> DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetPostResponse>>> FilterPost(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetPostResponse>> GetPostById(int postId)
        {
            string remoteUrl = $"https://jsonplaceholder.typicode.com/posts/{postId}";
            var response = new ServiceResponse<GetPostResponse>();
            try
            {
                response.Data = await _client.GetFromJsonAsync<GetPostResponse>(remoteUrl);
                response.Message = "Successfully retrived post!";
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = $"Error retriving post!. Exception: {e.Message}";
                response.Success = false;
            }
            return response;
        }

        public Task<ServiceResponse<List<NestedPostResponse>>> GetPostComments(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetPostResponse>>> GetPosts()
        {
            string remoteUrl = "https://jsonplaceholder.typicode.com/posts";
            var response = new ServiceResponse<List<GetPostResponse>>();
            response.Data = await _client.GetFromJsonAsync<List<GetPostResponse>>(remoteUrl);

            if (response.Data != null)
            {
                response.Success = true;
                response.Message = "Successfully retrived data from backend service!";
            }
            else
            {
                response.Success = false;
                response.Message = "Error retriving posts. Please check service method!";
            }
            return response;
        }

        public Task<ServiceResponse<CreatePostResponse>> PatchPost(PostRequest post)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<CreatePostResponse>> UpdatePost(PostRequest post)
        {
            throw new NotImplementedException();
        }
    }
}
