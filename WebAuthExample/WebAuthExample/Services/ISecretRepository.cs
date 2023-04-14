

using WebAuthExample.Models;

namespace WebAuthExample.Services
{
    public class ISecretRepository
    {
        public List<Secret> secretList = new List<Secret>();

        public async Task SeedSecretListAsync()
        {
            await Task.Run(() =>
            {
                secretList.Add(new Secret
                {
                    Id = 1,
                    SercretName = "Dolphin",
                    SercretValue = "Dolphin's are really smart!",
                    CreatedDate = DateTime.Now,
                    Creator = "Default"
                });
                secretList.Add(new Secret
                {
                    Id = 2,
                    SercretName = "Cheetah",
                    SercretValue = "Cheetah's are really fast animals",
                    CreatedDate = DateTime.Now,
                    Creator = "Default"
                });
            });
        }

        public async Task<List<Secret>> GetSecretsAsync()
        {
            var secrets = Task.Run(() => secretList.ToList());
            return await secrets;
        }

        public async Task<Secret> GetSecretAsync(int id)
        {
            var secret = Task.Run(() =>
            secretList.FirstOrDefault(s => s.Id == id)
            );
            return await secret;
        }

        public async Task<List<Secret>> RemoveSecretAsync(int id)
        {
            await Task.Run(() =>
            {
                var secret = secretList.FirstOrDefault(s => s.Id == id);
                secretList.Remove(secret);
            });
            var secrets = GetSecretsAsync();
            return await secrets;
        }
    }
}
