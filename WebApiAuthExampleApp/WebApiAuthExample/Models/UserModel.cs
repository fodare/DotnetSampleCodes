namespace WebApiAuthExample.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<UserSecretModel>? UserSecrets { get; set; }
    }
}
