using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;
using WebApiAuthExample.Data;
using WebApiAuthExample.DTO;
using WebApiAuthExample.Models;
using WebApiAuthExample.Services;

namespace WebApiAuthExample.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SecretsController : Controller
    {
        private readonly SecretService _secretService;

        public SecretsController(SecretService secretService)
        {
            _secretService = secretService;
        }

        [HttpGet("GetSecrets", Name = ("GetSecrets"))]
        public async Task<ActionResult<SecretDto>> GetSecrets()
        {
            var response = await _secretService.GetSecrets();
            return Ok(response);
        }

        [HttpPost("CreateSecret", Name = "CreateSecret")]
        public async Task<ActionResult<SecretDto>> CreateSecret(SecretDto newSecret)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(
                c => c.Type == ClaimTypes.NameIdentifier).Value);

            var response = await _secretService.CreateSecret(newSecret);
            if (!response.Success)
            {
                return BadRequest("Error creating secret!");
            }
            return Ok(response);

        }

        [HttpGet("GetSecrets/{id}", Name = "GetScretById")]
        public async Task<ActionResult<UserSecretModel>> GetSecretById(int id)
        {
            var response = await _secretService.GetUserSecretById(id);
            if (response.Success is false)
            {
                return BadRequest("Error. Please check secret id!");
            }
            return Ok(response);
        }

        [HttpDelete("DeleteSecret/{id}", Name = "DeleteSercret")]
        public async Task<ActionResult> DeleteSecret(int id)
        {
            var response = await _secretService.DeleteUserSecret(id);
            if (response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
