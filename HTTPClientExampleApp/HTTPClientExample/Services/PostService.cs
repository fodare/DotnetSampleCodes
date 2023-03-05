using HTTPClientExample.Models.Requests;
using HTTPClientExample.Models.Responses;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
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

        public async Task<ServiceResponse<CreatePostResponse>> CreatePost(PostRequest post)
        {
            string remoteUrl = "https://jsonplaceholder.typicode.com/posts";
            var response = new ServiceResponse<CreatePostResponse>();
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using HttpResponseMessage responseMessage = await _client.PostAsJsonAsync(remoteUrl, post);
                response.Data = await responseMessage.Content.ReadFromJsonAsync<CreatePostResponse>();
                response.Message = "Successfully created new post!";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error while creating new Post. Error message: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> DeletePost(int postId)
        {
            string remoteUrl = "https://jsonplaceholder.typicode.com/posts/";
            var response = new ServiceResponse<string>();
            try
            {
                using HttpResponseMessage responseMessage = await _client.DeleteAsync($"{remoteUrl}{postId}");
                response.Data = await responseMessage.Content.ReadAsStringAsync();
                response.Success = true;
                response.Message = "Post deleted successfully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error while deleting Post. Error message{ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetPostResponse>>> FilterPost(int userId)
        {
            string remoteUrl = "https://jsonplaceholder.typicode.com/posts?userId=";
            var response = new ServiceResponse<List<GetPostResponse>>();
            try
            {
                _client.DefaultRequestHeaders.Clear();
                response.Data = await _client.GetFromJsonAsync<List<GetPostResponse>>($"{remoteUrl}{userId}");
                response.Success = true;
                response.Message = "Posts filtered successfully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error while retriving Posts. Error message: {ex.Message}";
            }
            return response;
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

        public async Task<ServiceResponse<List<NestedPostResponse>>> GetPostComments(int postId)
        {
            string remoteUrl = "https://jsonplaceholder.typicode.com/posts/";
            var response = new ServiceResponse<List<NestedPostResponse>>();
            try
            {
                _client.DefaultRequestHeaders.Clear();
                response.Data = await _client.GetFromJsonAsync<List<NestedPostResponse>>($"{remoteUrl}{postId}/comments");
                response.Success = true;
                response.Message = "Successfully retrived post comments!";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = $"Error while retriving Posts comments. Error message: {e.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetPostResponse>>> GetPosts()
        {
            string remoteUrl = "https://jsonplaceholder.typicode.com/posts";
            var response = new ServiceResponse<List<GetPostResponse>>();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("customHeader", "ssjdfhsdhj");
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

        public async Task<ServiceResponse<CreatePostResponse>> UpdatePost(PostRequest post, int id)
        {
            string remoteUrl = "https://jsonplaceholder.typicode.com/posts/";
            var response = new ServiceResponse<CreatePostResponse>();
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using HttpResponseMessage responseMessage = await _client.PutAsJsonAsync($"{remoteUrl}{id}", post);
                response.Data = await responseMessage.Content.ReadFromJsonAsync<CreatePostResponse>();
                response.Message = "Post updated successfully!";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = $"Error while updating Post. Error message: {ex.Message}";
                response.Success = true;
            }
            return response;
        }
    }
}
