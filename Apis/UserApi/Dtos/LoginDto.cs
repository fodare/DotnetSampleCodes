using System;
using System.Text.Json.Serialization;

namespace UserApi.Dtos
{
    public class LoginDto
    {
        [JsonPropertyName("email")]
        string Email { get; set; } = "";

        [JsonPropertyName("password")]
        string Password { get; set; } = "";
    }
}