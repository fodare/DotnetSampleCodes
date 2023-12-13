using System;
using APIBasics.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserApi.Dtos;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly DataContextDappar _dapper;
        private readonly IConfiguration _config;
        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            _dapper = new DataContextDappar(configuration);
        }

        [HttpPost("register", Name = "CreateAccount")]
        public IActionResult RegisterAccount([FromBody] UserRegDto newUser)
        {
            if (newUser.Password == newUser.PasswordConformation)
            {
                string sqlCommand = @$"SELECT Email FROM TutorialAppSchema.Auth
                    WHERE Email = '{newUser.Email}'";
                IEnumerable<string> users = _dapper.LoadData<string>(sqlCommand);
                if (users.Count() == 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest($"An account with {newUser.Email} already exist!");
                }

            }
            return BadRequest("Passwords do not match");

        }

        [HttpPost("login", Name = "GetAccessToken")]
        public IActionResult CreateAccessToken([FromBody] LoginDto userCreds)
        {
            return Ok();
        }
    }
}