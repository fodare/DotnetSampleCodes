using System;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace UserApi.Dtos
{
    public class UserRegDto
    {
        [JsonPropertyName("email")]
        string Email { get; set; } = "";

        [JsonPropertyName("password")]
        string Password { get; set; } = "";

        [JsonPropertyName("passwordConformation")]
        string PasswordConformation { get; set; } = "";
    }
}