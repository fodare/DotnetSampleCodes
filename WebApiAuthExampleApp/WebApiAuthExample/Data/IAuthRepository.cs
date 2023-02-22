using WebApiAuthExample.Models;

namespace WebApiAuthExample.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register (UserModel user, string password);
        Task<ServiceResponse<string>> Login (string username, string password);
        Task<bool> UserExists (string username);
    }
}
