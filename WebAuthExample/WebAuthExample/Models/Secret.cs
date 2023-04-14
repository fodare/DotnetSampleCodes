using System.ComponentModel.DataAnnotations;

namespace WebAuthExample.Models
{
    public class Secret
    {
        public int Id { get; set; }
        public string SercretName { get; set; }
        public string SercretValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Creator { get; set; }
    }
}
