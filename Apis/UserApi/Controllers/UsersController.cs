using APIBasics.Data;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Dtos;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        DataContextDappar _dappar;
        public UsersController(IConfiguration configuration)
        {
            _dappar = new DataContextDappar(configuration);
        }

        [HttpGet("service-health", Name = "ServiceHealth")]
        public IActionResult GetServiceHealth()
        {
            return Ok(_dappar.LoadDataSingle<DateTime>("SELECT GETDATE()"));
        }

        [HttpGet("users", Name = "GetUsers")]
        public IActionResult GetUsers()
        {
            string sqlCommand = "SELECT * FROM TutorialAppSchema.Users";
            var usersList = _dappar.LoadData<User>(sqlCommand);
            if (usersList != null)
            {
                return Ok(usersList);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Code error. Please report to service desk for further assistance." });
            }
        }

        [HttpGet("user/{userId}", Name = "GetUser")]
        public IActionResult GetUser(int userId)
        {
            var sqlCommand = $@"SELECT * FROM TutorialAppSchema.Users
                            WHERE UserId = '{userId}'";
            var queriedUser = _dappar.LoadDataSingle<User>(sqlCommand);
            if (queriedUser != null)
            {
                return Ok(queriedUser);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("user/{userId}", Name = "UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserDTO updatedUser, int userId)
        {
            string checkUserCommand = $@"SELECT [FirstName] FROM TutorialAppSchema.Users
                WHERE UserId = '{userId}'";

            string updateCommand = $@"UPDATE TutorialAppSchema.Users 
                SET
                [FirstName] = '{updatedUser.FirstName}', 
                [LastName] = '{updatedUser.LastName}', 
                [Email] = '{updatedUser.Email}', 
                [Gender] = '{updatedUser.Gender}'
                WHERE UserId = '{userId}'";
            var queriedUser = _dappar.LoadDataSingle<User>(checkUserCommand);
            if (queriedUser.FirstName == null)
            {
                return BadRequest("Can not find user!");
            }
            else
            {
                bool userUpdated = _dappar.UpdateSql(updateCommand);
                if (userUpdated)
                {
                    return Ok("User updated!");
                }
                else
                {
                    return BadRequest("Error updating user!");
                }
            }
        }

        [HttpPost("user/create", Name = "CreateUser")]
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO newUser)
        {
            string sqlCommand = @$"INSERT INTO TutorialAppSchema.Users(
                [FirstName], 
                [LastName], 
                [Email], 
                [Gender], 
                [Active]
            ) VALUES ('{newUser.FirstName}', '{newUser.LastName}', '{newUser.Email}', '{newUser.Gender} ', 0);";
            bool userCreated = _dappar.ExecuteSql(sqlCommand);
            if (userCreated)
            {
                return Ok("User createed!");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Code error. Please report to service desk for further assistance." });
            }
        }

        [HttpDelete("user/{userId}", Name = "DeleteUser")]
        public IActionResult DeleteUser(int userId)
        {
            string sqlDeleteCommand = @$"DELETE FROM TutorialAppSchema.Users
                WHERE UserId = '{userId}'";

            string checkUserCommand = $@"SELECT [FirstName] FROM TutorialAppSchema.Users
                WHERE UserId = '{userId}'";

            var queriedUser = _dappar.LoadDataSingle<User>(checkUserCommand);
            if (queriedUser.FirstName == null)
            {
                return NotFound("User id not found");
            }
            else
            {
                bool userDeleted = _dappar.ExecuteSql(sqlDeleteCommand);
                if (userDeleted)
                {
                    return Ok("User deleted succeffully!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Code error. Please report to service desk for further assistance." });
                }
            }
        }
    }
}