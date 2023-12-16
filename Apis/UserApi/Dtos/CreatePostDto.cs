using System;
using System.Text.Json.Serialization;

namespace UserApi.Dtos
{
    public class CreatePostDto
    {
        [JsonPropertyName("postTitle")]
        public string PostTitle { get; set; } = "";

        [JsonPropertyName("postContent")]
        public string PostContent { get; set; } = "";
    }
}
