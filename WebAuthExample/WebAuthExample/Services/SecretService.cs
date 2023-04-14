using WebAuthExample.Models;

namespace WebAuthExample.Services
{
    public class SecretService : ISecretInterface
    {
        private readonly ISecretRepository _secretrepo;

        public SecretService(ISecretRepository secretRepository)
        {
            _secretrepo = secretRepository;
        }

        public async Task<Secret> CreateSecretasync(Secret newSecret)
        {
            throw new NotImplementedException();
        }

        public Task<List<Secret>> DeleteSecretAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Secret> GetSrecretAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Secret>> GetSrecretsAsync()
        {
            var result = await _secretrepo.GetSecretsAsync();
            if (result is null || result.Count <= 0)
            {
                await _secretrepo.SeedSecretListAsync();
                result = await _secretrepo.GetSecretsAsync();
            }
            return result;
        }
    }
}
