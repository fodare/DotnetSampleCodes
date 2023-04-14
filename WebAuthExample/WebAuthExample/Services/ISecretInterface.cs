using WebAuthExample.Models;

namespace WebAuthExample.Services
{
    public interface ISecretInterface
    {
        Task<List<Secret>> GetSrecretsAsync();
        Task<Secret> GetSrecretAsync(int id);
        Task<Secret> CreateSecretasync(Secret newSecret);
        Task<List<Secret>> DeleteSecretAsync(int id);
    }
}
