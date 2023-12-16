using System;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Net.Http.Headers;

namespace UserApi.Dtos
{
    public class LoginConfirmDto
    {
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
    }
}