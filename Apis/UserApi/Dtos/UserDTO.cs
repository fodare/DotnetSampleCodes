using System;
using System.Text.Json.Serialization;
namespace webapi.Dtos
{
    public class UserDTO
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = "";

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = "";

        [JsonPropertyName("email")]
        public string Email { get; set; } = "";

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = "";

        [JsonPropertyName("isActive")]
        public bool Active { get; set; }
    }
}