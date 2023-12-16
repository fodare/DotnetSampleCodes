using System;
using System.Text.Json.Serialization;

namespace UserApi.Models
{
    public class Post
    {
        [JsonPropertyName("postId")]
        public int PostId { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("postTitle")]
        public string PostTitle { get; set; } = "";

        [JsonPropertyName("postContent")]
        public string PostContent { get; set; } = "";

        [JsonPropertyName("createDate")]
        public DateTime PostCreationDate { get; set; }

        [JsonPropertyName("updateDate")]
        public DateTime LastUpdateddate { get; set; }
    }
}