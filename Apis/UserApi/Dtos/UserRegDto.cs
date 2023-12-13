using System;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace UserApi.Dtos
{
    public class UserRegDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";

        [JsonPropertyName("password")]
        public string Password { get; set; } = "";

        [JsonPropertyName("passwordConformation")]
        public string PasswordConformation { get; set; } = "";
    }
}