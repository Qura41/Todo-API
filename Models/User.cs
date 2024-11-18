using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TodoAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set;}
        [Required]
        public string? Login { get; set;}
        [Required]
        public string? Password { get; set; }
        public string? Nickname { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User()
        {
            Nickname = "User";
        }
    }
}