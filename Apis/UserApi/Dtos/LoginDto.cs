using System;
using System.Text.Json.Serialization;

namespace UserApi.Dtos
{
    public class LoginDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";

        [JsonPropertyName("password")]
        public string Password { get; set; } = "";
    }
}