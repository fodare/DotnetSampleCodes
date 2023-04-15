using System.ComponentModel.DataAnnotations;

namespace WebAuthExample.Models
{
    public class User
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
