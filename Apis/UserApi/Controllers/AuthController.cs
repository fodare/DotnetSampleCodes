using System;
using System.Security.Cryptography;
using System.Text;
using APIBasics.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
                    byte[] randomPasswordSalt = new byte[128 / 8];

                    using (RandomNumberGenerator randomNumber = RandomNumberGenerator.Create())
                    {
                        randomNumber.GetNonZeroBytes(randomPasswordSalt);
                    }

                    string passwordSalt = _config.GetSection("AppSettings:PasswordKey").Value
                        + Convert.ToBase64String(randomPasswordSalt);

                    byte[] passwordHash = KeyDerivation.Pbkdf2(
                        password: newUser.Password,
                        salt: Encoding.ASCII.GetBytes(passwordSalt),
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8
                    );

                    string sqlAddauth = @$"INSERT INTO TutorialAppSchema.Auth(
                        Email,PasswordHash, PasswordSalt
                    )VALUES ('{newUser.Email}',{passwordHash},{passwordSalt})";
                    _dapper.ExecuteSql(sqlAddauth);
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