namespace WebApiAuthExample.Models
{
    public class UserSecretModel
    {
        public int Id { get; set; }
        public string SecreatMessage { get; set; }
        public DateTime CreatedDate { get; set; }

        public UserModel? User { get; set; }
    }
}
    