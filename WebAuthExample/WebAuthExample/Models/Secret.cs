using System.ComponentModel.DataAnnotations;

namespace WebAuthExample.Models
{
    public class Secret
    {
        public int Id { get; set; }
        [Required]
        public string SercretName { get; set; }
        [Required]
        public string SercretValue { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string Creator { get; set; }
    }
}
