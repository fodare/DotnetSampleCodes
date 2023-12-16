using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using APIBasics.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using UserApi.Dtos;
using webapi.Models;

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
                // string sqlCommand = @$"SELECT Email FROM TutorialAppSchema.Auth
                //     WHERE Email = '{newUser.Email}'";
                // IEnumerable<string> users = _dapper.LoadData<string>(sqlCommand);
                // if (users.Count() == 0)
                // {
                //     byte[] randomPasswordSalt = new byte[128 / 8];

                //     using (RandomNumberGenerator randomNumber = RandomNumberGenerator.Create())
                //     {
                //         randomNumber.GetNonZeroBytes(randomPasswordSalt);
                //     }

                //     string passwordSalt = _config.GetSection("AppSettings:PasswordKey").Value
                //         + Convert.ToBase64String(randomPasswordSalt);

                //     byte[] passwordHash = KeyDerivation.Pbkdf2(
                //         password: newUser.Password,
                //         salt: Encoding.ASCII.GetBytes(passwordSalt),
                //         prf: KeyDerivationPrf.HMACSHA256,
                //         iterationCount: 100000,
                //         numBytesRequested: 256 / 8
                //     );

                //     string sqlAddauth = @$"INSERT INTO TutorialAppSchema.Auth(
                //         Email,PasswordHash, PasswordSalt
                //     )VALUES ('{newUser.Email}',{passwordHash},{passwordSalt})";
                //     _dapper.ExecuteSql(sqlAddauth);
                //     return Ok();
                // }
                // else
                // {
                //     return BadRequest($"An account with {newUser.Email} already exist!");
                // }

                string sqlAddUser = @$"INSERT INTO TutorialAppSchema.AuthTable(
                    [Email], [Password], [PasswordConformation]
                ) VALUES ('{newUser.Email}', '{newUser.Password}', '{newUser.PasswordConformation}')";

                string sqlAddToUserTable = $@"INSERT INTO TutorialAppSchema.Users(
                    [FirstName],[LastName],[Email],[Gender]
                ) VALUES ('{newUser.FirstName}','{newUser.LastName}','{newUser.Email}','{newUser.Gender}');";

                bool userAddded = _dapper.ExecuteSql(sqlAddUser);
                if (userAddded)
                {
                    _dapper.ExecuteSql(sqlAddToUserTable);
                    return Ok("User account added sucessfully!");
                }
                return StatusCode(500, "Error adding user!. Please try again!");
            }
            return BadRequest("Passwords do not match");
        }

        [HttpPost("login", Name = "GetAccessToken")]
        public IActionResult CreateAccessToken([FromBody] LoginDto userCreds)
        {
            string sqlCheckUser = @$"SELECT [Email], [Password], [PasswordConformation] FROM
                TutorialAppSchema.AuthTable WHERE [Email] = '{userCreds.Email}'";

            IEnumerable<UserRegDto> foundUsers = _dapper.LoadData<UserRegDto>(sqlCheckUser);

            if (foundUsers.Any())
            {
                foreach (var user in foundUsers)
                {
                    if (user.Password == userCreds.Password)
                    {
                        string sqlGetUserId = @$"SELECT 
                        [UserId] FROM TutorialAppSchema.Users
                            WHERE Email = '{user.Email}'";

                        int LoginUserId = _dapper.LoadDataSingle<int>(sqlGetUserId);

                        string newJwtToken = CreateJwtToken(userEmail: user.Email, userId: LoginUserId);

                        return Ok(
                            new Dictionary<string, string>{
                                {"token", $"{newJwtToken}"}
                            });
                    }
                    else
                    {
                        return BadRequest("Invalid Password!");
                    }
                }
            }
            return BadRequest("Account not found!");
        }

        private string CreateJwtToken(int userId, string userEmail)
        {
            Claim[] claims = new Claim[]{
                new("userId", userId.ToString()),
                new("userEmail", userEmail)
            };

            string? securityTokenKey = _config.GetSection("AppSettings:TokenKey").Value;

            SymmetricSecurityKey tokenKey = new(
                Encoding.UTF8.GetBytes(securityTokenKey ?? "")
            );

            SigningCredentials credentials = new(tokenKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddMinutes(15)
            };

            JwtSecurityTokenHandler tokenHandler = new();

            SecurityToken jwtToken = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(jwtToken);

        }
    }
}