using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiAuthExample.Data;
using WebApiAuthExample.DTO;
using WebApiAuthExample.Models;

namespace WebApiAuthExample.Services
{
    public class SecretService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecretService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<UserSecretModel>>> GetSecrets()
        {
            var response = new ServiceResponse<List<UserSecretModel>>();
            response.Data = await _context.UserSercrets
                .Where(u => u.User.Id == GetUserId()).ToListAsync();
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<SecretDto>> CreateSecret(SecretDto newUserSecret)
        {
            var response = new ServiceResponse<SecretDto>();
            var userSecret = new UserSecretModel();
            userSecret.SecreatMessage = newUserSecret.userSecret;
            userSecret.CreatedDate = DateTime.Now;
            userSecret.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.UserSercrets.Add(userSecret);
            await _context.SaveChangesAsync();
            response.Success = true;
            response.Message = "Secret saved successfully!";
            response.Data = newUserSecret;
            return response;
        }


        public async Task<ServiceResponse<UserSecretModel>> GetUserSecretById(int id)
        {
            var response = new ServiceResponse<UserSecretModel>();
            response.Data = await _context.UserSercrets.FirstOrDefaultAsync(
                s => s.Id == id && s.User.Id == GetUserId());
            if (response.Data is null)
            {
                response.Success = false;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteUserSecret(int id)
        {
            var response = new ServiceResponse<string>();
            try
            {
                UserSecretModel secretModel = await _context.UserSercrets.FirstOrDefaultAsync(
                    s => s.Id == id && s.User.Id == GetUserId());
                _context.UserSercrets.Remove(secretModel);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Secret deleted successfully!";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Error deleting secret. Please try again!";
                Console.WriteLine($"Exception deleting secret from DB. Exception message: " +
                    $"{e.Message}");
            }
            return response;
        }


        // Service support methods

        // Get user id / Claim identifier
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
