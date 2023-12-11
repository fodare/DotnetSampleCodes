using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIBasics.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Dtos;
using webapi.Models;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserEFController : ControllerBase
    {
        DataContextEF _ef;
        IMapper _mapper;
        public UserEFController(IConfiguration configuration)
        {
            _ef = new DataContextEF(configuration);
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>();
            }));
        }

        [HttpGet("getusers", Name = "GetAllUsers")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var usersList = _ef.Users.ToList<User>();
            if (usersList == null)
            {
                return StatusCode(500, "Error retiving users from DB");
            }
            else
            {
                return Ok(usersList);
            }
        }

        [HttpGet("{userId}", Name = "GetUserById")]
        public async Task<ActionResult<User>> GetUserAsync(int userId)
        {
            User? user = await _ef.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPut("update/{userId}", Name = "UpdateUserById")]
        public async Task<ActionResult> UpdateUserInfoAsync([FromBody] UserDTO newUser, int userId)
        {
            var queriedUser = await _ef.Users.FirstOrDefaultAsync<User>(u => u.UserId == userId);
            if (queriedUser.FirstName != null)
            {
                User updatedUser = _mapper.Map<User>(newUser);
                _ef.Users.Update(updatedUser);
                return Ok("User updated!");
            }
            else
            {
                return NotFound("User not found");
            }
        }

        [HttpDelete("delete/{userId}", Name = "DelteUser")]
        public async Task<IActionResult> RemoveUserAsync(int userId)
        {
            var queriedUser = await _ef.Users.FirstOrDefaultAsync<User>(u => u.UserId == userId);
            if (queriedUser.FirstName != null)
            {
                _ef.Users.Remove(queriedUser);
                return Ok("User refomved successfully!");
            }
            else
            {
                return NotFound("User not found!");
            }
        }

        [HttpPost("createUser", Name = "CreateNewUser")]
        public IActionResult AddUser([FromBody] UserDTO newUser)
        {
            var userToDb = _mapper.Map<User>(newUser);
            _ef.Users.Add(userToDb);
            return Ok("Users added to DB!");
        }
    }
}