using Microsoft.AspNetCore.Mvc;
using WebApiAuthExample.Data;
using WebApiAuthExample.DTO.User;
using WebApiAuthExample.Models;

namespace WebApiAuthExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register", Name = "Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserDto newUser)
        {
            var response = await _authRepo.Register(new UserModel { UserName = newUser.UserName },
                newUser.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        [HttpPost("login", Name = "login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserDto userAccount)
        {
            var response = new ServiceResponse<string>();
            if (userAccount is null)
            {
                response.Success = false;
                return BadRequest(response);
            }
            else
            {
                var userResponseData = await _authRepo.Login(userAccount.UserName, userAccount.Password);
                if (!userResponseData.Success)
                {
                    return BadRequest(userResponseData);
                } else
                {
                    return Ok(userResponseData);
                }
            }
        }
    }
}
