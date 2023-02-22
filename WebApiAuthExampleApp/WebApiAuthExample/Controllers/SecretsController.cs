using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;
using WebApiAuthExample.Data;
using WebApiAuthExample.DTO;
using WebApiAuthExample.Models;

namespace WebApiAuthExample.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SecretsController : Controller
    {
        private readonly DataContext _context;

        public SecretsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetSecrets", Name = ("GetSecrets"))]
        public async Task<ActionResult<SecretDto>> GetSecrets()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(
                c => c.Type == ClaimTypes.NameIdentifier).Value);

            Console.WriteLine($"Claims id: {id}");
            var response = await _context.UserSercrets.ToListAsync();
            return Ok(response);
        }

        [HttpPost("CreateSecret", Name = "CreateSecret")]
        public async Task<ActionResult<SecretDto>> CreateSecret(SecretDto newSecret)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(
                c => c.Type == ClaimTypes.NameIdentifier).Value);
            var userSecret = new UserSecretModel();
            userSecret.SecreatMessage = newSecret.userSecret;
            userSecret.CreatedDate = DateTime.Now;
            /*userSecret.User.Id = userId;*/
            _context.Add(userSecret);
            await _context.SaveChangesAsync();
            return CreatedAtAction("CreateSecret", newSecret);
        }
    }
}
